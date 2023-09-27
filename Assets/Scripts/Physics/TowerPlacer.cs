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
        [SerializeField] private string placeButtontName = "Place Defender";

        [Header("Feedbacks")]
        [SerializeField] private Material failFeedback;
        [SerializeField] private Material successFeedback;

        public event Action<DefenderTower> OnPlaceTower;

        public bool HasValidPosition { get; private set; }

        private Camera mainCamera;
        private DefenderTower tower;

        private void Awake() => mainCamera = Camera.main;

        private void Update()
        {
            UpdatePositionUsingMouse();

            /*if (tower != null)
            {
                UpdatePassengerMaterialFeedback();
                UpdatePlaceInput();
            }*/
        }

        public void SetPassenger(DefenderTower tower)
        {
            this.tower = tower;
            this.tower.transform.SetParent(transform);
            this.tower.transform.localPosition = towerOffset;
        }

        private void UpdatePositionUsingMouse()
        {
            var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            var hasHit = UnityPhysics.Raycast(cameraRay, out RaycastHit hit, maxDistance, collision);

            HasValidPosition = hasHit && CanPlaceTower();

            if (!hasHit) return;

            transform.position = hit.point;
        }

        private void UpdatePlaceInput()
        {
            var hasInput = Input.GetButtonDown(placeButtontName);
            if (hasInput && HasValidPosition) PlaceTower();
        }

        private void UpdatePassengerMaterialFeedback()
        {
            var material = HasValidPosition ? successFeedback : failFeedback;
            tower.Material.SetMaterials(material);
        }

        private void PlaceTower()
        {
            tower.Material.ResetMaterials();

            tower.transform.SetParent(null);
            tower.transform.position -= towerOffset;
            transform.position = Vector3.zero;

            OnPlaceTower?.Invoke(tower);
            tower = null;
        }

        private bool CanPlaceTower() => tower.CanPlace();
    }
}