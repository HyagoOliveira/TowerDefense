using UnityEngine;
using UnityEngine.AI;
using TowerDefense.VisualEffects;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(MaterialChanger))]
    public sealed class DefenderTower : MonoBehaviour
    {
        [SerializeField] private NavMeshObstacle obstacle;

        [field: SerializeField] public MaterialChanger MaterialChanger { get; private set; }

        public string Name => gameObject.name;
        public int Price => 0;

        private void Reset()
        {
            MaterialChanger = GetComponent<MaterialChanger>();
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