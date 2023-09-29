using System;
using UnityEngine;
using UnityEngine.AI;
using TowerDefense.Physics;
using TowerDefense.VisualEffects;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TowerDetector))]
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(MaterialReplacer))]
    public sealed class DefenderTower : MonoBehaviour
    {
        [SerializeField] private string displayname = "Tower";
        [SerializeField, Min(0)] private int purchasePrice = 10;
        [SerializeField, Min(0)] private int baseUpgradePrice = 10;

        [Space]
        [SerializeField] private TowerDetector detector;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private MaterialReplacer material;

        public static event Action<DefenderTower> OnClicked;

        public string DysplayName => displayname;
        public int PurchasePrice => purchasePrice;
        public int UpgradePrice => PurchasePrice + baseUpgradePrice * upgrades;

        public TowerDetector Detector => detector;
        public MaterialReplacer Material => material;

        private bool isPlaced;
        private int upgrades;

        private void Reset()
        {
            detector = GetComponent<TowerDetector>();
            boxCollider = GetComponent<BoxCollider>();
            material = GetComponent<MaterialReplacer>();
        }

        private void OnMouseDown()
        {
            if (isPlaced) OnClicked?.Invoke(this);
        }

        public bool CanPlace()
        {
            var bounds = boxCollider.bounds;

            // Avoids collision with itself
            boxCollider.enabled = false;
            var hasCollision = UnityEngine.Physics.CheckBox(bounds.center, bounds.extents);
            boxCollider.enabled = true;

            return !hasCollision;
        }

        internal void Place()
        {
            isPlaced = true;
            Detector.enabled = true;
            Material.ResetMaterials();
            transform.SetParent(null);
        }
    }
}