using UnityEngine;
using System.Collections;

namespace TowerDefense.Gameplay
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Tower Defense/Enemy Wave", order = 110)]
    public sealed class EnemyWave : ScriptableObject
    {
        [SerializeField] private Enemy prefab;
        [SerializeField, Min(0)] private int quantity = 1;
        [SerializeField, Min(0f)] private float timeBetweenSpawns = 1f;

        public IEnumerator SpawnRoutine(
            Vector3 position,
            Vector3 destination,
            Quaternion rotation,
            int quantityMultiplier = 1
        )
        {
            quantity *= quantityMultiplier;

            var remainSpawns = quantity;
            var waitTime = new WaitForSeconds(timeBetweenSpawns);

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