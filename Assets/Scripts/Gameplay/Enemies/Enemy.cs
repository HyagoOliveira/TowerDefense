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

        public float Speed
        {
            get => agent.speed;
            set => agent.speed = value;
        }

        private void Reset() => agent = GetComponent<NavMeshAgent>();
    }
}