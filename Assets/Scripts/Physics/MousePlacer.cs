using UnityEngine;
using UnityPhysics = UnityEngine.Physics;

namespace TowerDefense.Physics
{
    [DisallowMultipleComponent]
    public sealed class MousePlacer : MonoBehaviour
    {
        [Header("Physics Detection")]
        [SerializeField] private LayerMask collision;
        [SerializeField, Min(0f)] private float maxDistance = 100F;

        [Header("Input")]
        [SerializeField] private string placePassagerButtontName = "Place Defender";

        [Header("Passenger")]
        [SerializeField] private Vector3 passengerOffset = Vector3.up * 0.5F;

        [Header("Feedbacks")]
        [SerializeField] private Material failFeedback;
        [SerializeField] private Material successFeedback;

        public bool HasValidPosition { get; private set; }

        private Camera mainCamera;
        private IPassengerable passenger;

        private void Awake() => mainCamera = Camera.main;
        private void Start() => enabled = false; // For optimization

        private void Update()
        {
            UpdatePositionUsingMouse();

            if (passenger != null)
            {
                UpdatePassengerMaterialFeedback();
                UpdatePlaceInput();
            }
        }

        public void SetPassenger(IPassengerable passenger)
        {
            this.passenger = passenger;
            this.passenger.transform.SetParent(transform);
            this.passenger.transform.localPosition = passengerOffset;

            enabled = true; // For optimization
        }

        private void UpdatePositionUsingMouse()
        {
            var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            var hasHit = UnityPhysics.Raycast(cameraRay, out RaycastHit hit, maxDistance, collision);

            HasValidPosition = hasHit && CanPlacePassenger();

            if (!hasHit) return;

            transform.position = hit.point;
        }

        private void UpdatePlaceInput()
        {
            var hasInput = Input.GetButtonDown(placePassagerButtontName);
            if (hasInput && HasValidPosition) PlacePassager();
        }

        private void UpdatePassengerMaterialFeedback()
        {
            var material = HasValidPosition ? successFeedback : failFeedback;
            passenger.MaterialChanger.SetMaterials(material);
        }

        private void PlacePassager()
        {
            passenger.MaterialChanger.ResetMaterials();

            passenger.transform.SetParent(null);
            passenger.transform.position -= passengerOffset;

            passenger = null;

            enabled = false; // For optimization
        }

        private bool CanPlacePassenger() => passenger.CanPlace();
    }
}