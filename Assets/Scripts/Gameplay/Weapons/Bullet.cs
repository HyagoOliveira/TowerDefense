using UnityEngine;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float speed = 1f;
        [SerializeField, Min(0f)] private float timeAlive = 10f;
        [SerializeField] private Vector3 direction = Vector3.forward;

        private void Start() => Invoke(nameof(Destroy), timeAlive);
        private void FixedUpdate() => UpdateMovement();

        public void Fire(float speed, Vector3 direction)
        {
            this.speed = speed;
            this.direction = direction;
        }

        private void UpdateMovement()
        {
            var speedPerSeconds = speed * Time.deltaTime;
            var velocity = speedPerSeconds * direction;

            transform.position += velocity;
        }

        private void Destroy() => Destroy(gameObject);
    }
}