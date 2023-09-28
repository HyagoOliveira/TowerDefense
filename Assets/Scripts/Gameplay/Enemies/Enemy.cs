using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField, Min(0)] private int damage = 10;

        public float Speed
        {
            get => agent.speed;
            set => agent.speed = value;
        }

        public int Damage => damage;

        private void Reset() => agent = GetComponent<NavMeshAgent>();

        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
    }
}