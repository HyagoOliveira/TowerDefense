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
        [SerializeField] private TowerDetector detector;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private MaterialReplacer material;

        public int Price => 10;
        public string Name => gameObject.name;

        public TowerDetector Detector => detector;
        public MaterialReplacer Material => material;

        private void Reset()
        {
            detector = GetComponent<TowerDetector>();
            boxCollider = GetComponent<BoxCollider>();
            material = GetComponent<MaterialReplacer>();
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
    }
}