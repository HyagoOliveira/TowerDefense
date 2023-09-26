using UnityEngine;
using TowerDefense.Physics;

namespace TowerDefense.Managers
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1)]
    public sealed class MatchManager : MonoBehaviour
    {
        [SerializeField] private MatchSettings settings;
        [SerializeField] private MousePlacer placer;

        private void Reset() => placer = GetComponentInChildren<MousePlacer>();
        private void Start() => settings.Initialize(placer);
    }
}