using UnityEngine;
using System.Collections;
using System;

namespace TowerDefense.Gameplay
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Tower Defense/Enemy Wave", order = 110)]
    public sealed class EnemyWave : ScriptableObject
    {
        [SerializeField] private Enemy prefab;
        [SerializeField, Min(0)] private int quantity = 1;
        [SerializeField, Min(0f)] private float timeBetweenSpawns = 1f;

        public event Action OnSpawnStarted;

        public int AdditionalQuantity { get; set; } = 0;

        public IEnumerator SpawnRoutine(
            Vector3 position,
            Vector3 destination,
            Quaternion rotation
        )
        {
            var remainSpawns = quantity + AdditionalQuantity;
            var waitTime = new WaitForSeconds(timeBetweenSpawns);

            OnSpawnStarted?.Invoke();

            do
            {
                var enemy = Instantiate(prefab, position, rotation);
                enemy.SetDestination(destination);

                yield return waitTime;
                remainSpawns--;

            } while (remainSpawns > 0);
        }
    }
}