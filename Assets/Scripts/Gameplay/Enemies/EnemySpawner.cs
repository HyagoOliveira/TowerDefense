using UnityEngine;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private Transform destination;
    }
}