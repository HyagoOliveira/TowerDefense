using UnityEngine;
using UnityPhysics = UnityEngine.Physics;

namespace TowerDefense.Physics
{
    [DisallowMultipleComponent]
    public sealed class MousePlacer : MonoBehaviour
    {
        [SerializeField] private LayerMask collision;

        private Camera mainCamera;
        private const float maxDistance = 100F;

        private void Awake() => mainCamera = Camera.main;
        private void Start() => enabled = false;

        private void Update()
        {
            UpdatePositionUsingMouse();
        }

        private void UpdatePositionUsingMouse()
        {
            var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            var hasHit = UnityPhysics.Raycast(cameraRay, out RaycastHit hit, maxDistance, collision);

            if (!hasHit) return;

            transform.position = hit.point;
        }
    }
}