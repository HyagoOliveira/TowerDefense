using System;
using UnityEngine;
using TowerDefense.Gameplay;
using UnityPhysics = UnityEngine.Physics;

namespace TowerDefense.Physics
{
    [DisallowMultipleComponent]
    public sealed class TowerPlacer : MonoBehaviour
    {
        [Header("Physics Detection")]
        [SerializeField] private LayerMask collision;
        [SerializeField, Min(0f)] private float maxDistance = 100F;
        [SerializeField] private Vector3 towerOffset = Vector3.up * 0.5F;

        [Header("Input")]
        [SerializeField] private string confirmButtontName = "Confirm Tower Placement";
        [SerializeField] private string cancelButtontName = "Cancel Tower Placement";

        [Header("Feedbacks")]
        [SerializeField] private Material placeable;
        [SerializeField] private Material notPlaceable;

        public event Action<DefenderTower> OnTowerPlaced;

        public bool HasValidPosition { get; private set; }

        private Camera mainCamera;
        private DefenderTower tower;

        private void Awake() => mainCamera = Camera.main;

        private void Update()
        {
            UpdatePositionUsingMouse();
            UpdateTowerMaterial();
            UpdateInput();
        }

        public void SetTower(DefenderTower tower)
        {
            this.tower = tower;
            this.tower.Detector.enabled = false;
            this.tower.transform.SetParent(transform);
            this.tower.transform.localPosition = towerOffset;

            Enable();
        }

        private void UpdatePositionUsingMouse()
        {
            var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            var hasHit = UnityPhysics.Raycast(cameraRay, out RaycastHit hit, maxDistance, collision);

            if (hasHit) transform.position = hit.point;

            HasValidPosition = hasHit && HasValidUpNormal(hit.normal) && tower.CanPlace();
        }

        private void UpdateInput()
        {
            if (Input.GetButtonDown(confirmButtontName))
            {
                if (HasValidPosition) PlaceTower();
            }
            else if (Input.GetButtonDown(cancelButtontName)) CancelTower();
        }

        private void UpdateTowerMaterial()
        {
            var material = HasValidPosition ? placeable : notPlaceable;
            tower.Material.SetMaterials(material);
        }

        private void PlaceTower()
        {
            tower.Detector.enabled = true;
            tower.Material.ResetMaterials();
            tower.transform.SetParent(null);
            tower.transform.position -= towerOffset;

            OnTowerPlaced?.Invoke(tower);
            Disable();
        }

        private void CancelTower()
        {
            Destroy(tower.gameObject);
            Disable();
        }

        private void Enable() => gameObject.SetActive(true);

        private void Disable()
        {
            tower = null;
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        private static bool HasValidUpNormal(Vector3 normal) => Vector3.Dot(normal, Vector3.up) > 0.99f;
    }
}