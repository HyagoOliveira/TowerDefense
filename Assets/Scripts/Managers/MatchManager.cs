using UnityEngine;

namespace TowerDefense.Managers
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1)]
    public sealed class MatchManager : MonoBehaviour
    {
        [SerializeField] private MatchSettings settings;

        private void Start() => settings.ResetValues();
    }
}