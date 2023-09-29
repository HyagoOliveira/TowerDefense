using UnityEngine;
using UnityEngine.AI;
using TowerDefense.Managers;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private MatchSettings settings;
        [SerializeField] private NavMeshAgent pathAgent;
        [SerializeField] private Transform origin;
        [SerializeField] private Transform destination;

        public Vector3 Origin => origin.position;
        public Vector3 Destination => destination.position;

        private void OnEnable() => settings.OnEnemyWaveSpawned += HandleEnemyWaveSpawned;
        private void OnDisable() => settings.OnEnemyWaveSpawned -= HandleEnemyWaveSpawned;

        public bool IsAbleToCompletePath()
        {
            var path = new NavMeshPath();
            return
                pathAgent.CalculatePath(Destination, path) &&
                path.status == NavMeshPathStatus.PathComplete;
        }

        private void HandleEnemyWaveSpawned(EnemyWave wave)
        {
            var direction = (Destination - Origin).normalized;
            var orientation = Quaternion.LookRotation(direction);

            StopAllCoroutines();
            StartCoroutine(wave.SpawnRoutine(Origin, Destination, orientation));
        }
    }
}