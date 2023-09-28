using UnityEngine;
using TowerDefense.Managers;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private MatchSettings settings;
        [SerializeField] private Transform origin;
        [SerializeField] private Transform destination;

        private void OnEnable() => settings.OnEnemyWaveSpawned += HandleEnemyWaveSpawned;
        private void OnDisable() => settings.OnEnemyWaveSpawned -= HandleEnemyWaveSpawned;

        private void HandleEnemyWaveSpawned(EnemyWave wave)
        {
            var direction = (destination.position - origin.position).normalized;
            var orientation = Quaternion.LookRotation(direction);

            StopAllCoroutines();
            StartCoroutine(wave.SpawnRoutine(origin.position, destination.position, orientation));
        }
    }
}