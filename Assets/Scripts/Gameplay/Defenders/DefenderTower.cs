using UnityEngine;
using UnityEngine.AI;
using TowerDefense.VisualEffects;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(MaterialReplacer))]
    public sealed class DefenderTower : MonoBehaviour
    {
        [SerializeField] private NavMeshObstacle obstacle;
        [SerializeField] private MaterialReplacer material;

        public int Price => 0;
        public string Name => gameObject.name;
        public MaterialReplacer Material => material;

        private void Reset()
        {
            material = GetComponent<MaterialReplacer>();
            obstacle = GetComponentInChildren<NavMeshObstacle>();
        }

        public bool CanPlace()
        {
            var distance = obstacle.height;
            var position = transform.position + Vector3.up * distance;
            var hasCollision = UnityEngine.Physics.SphereCast(
                position,
                obstacle.radius,
                direction: Vector3.down,
                out RaycastHit _,
                distance
            );

            return !hasCollision;
        }
    }
}