using UnityEngine;
using TowerDefense.Physics;
using TowerDefense.VisualEffects;
using UnityPhysics = UnityEngine.Physics;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MaterialChanger))]
    public sealed class Defender : MonoBehaviour, IPassengerable
    {
        [SerializeField] private Collider collider;
        [SerializeField] private int price = 0;

        [field: SerializeField] public MaterialChanger MaterialChanger { get; private set; }

        private void Reset()
        {
            collider = GetComponentInChildren<Collider>();
            MaterialChanger = GetComponent<MaterialChanger>();
        }

        public bool CanPlace()
        {
            var bounds = collider.bounds;
            var height = bounds.size.y;
            var position = bounds.center + Vector3.up * height;
            var hasCollision = UnityPhysics.BoxCast(
                position,
                bounds.extents,
                direction: Vector3.down,
                orientation: Quaternion.identity,
                maxDistance: height
            );

            return !hasCollision;
        }

        public string GetName() => gameObject.name;
        public string GetPrice() => price.ToString("D2");
    }
}