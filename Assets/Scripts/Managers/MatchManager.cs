using UnityEngine;
using TowerDefense.Physics;

namespace TowerDefense.Managers
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(ExecutionOrder.MANAGERS)]
    public sealed class MatchManager : MonoBehaviour
    {
        [SerializeField] private MatchSettings settings;
        [SerializeField] private MousePlacer placer;

        private void Reset() => placer = GetComponentInChildren<MousePlacer>();
        private void Awake() => settings.Initialize(placer);
        private void Start() => settings.Start();
    }
}