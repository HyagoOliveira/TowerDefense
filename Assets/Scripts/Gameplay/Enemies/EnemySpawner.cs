using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform origin;
        [SerializeField] private Transform destination;

        public void Spawn()
        {
            var agent = Instantiate(prefab).GetComponent<NavMeshAgent>();

            agent.transform.position = origin.position;
            agent.SetDestination(destination.position);
        }
    }
}