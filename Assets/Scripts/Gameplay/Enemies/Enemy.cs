using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private void Reset() => agent = GetComponent<NavMeshAgent>();
    }
}