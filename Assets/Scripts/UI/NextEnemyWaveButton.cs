using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Managers;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class NextEnemyWaveButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private MatchSettings settings;

        private void Reset() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(HandleButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(HandleButtonClicked);

        private void HandleButtonClicked() => settings.SpawnNextEnemyWave();
    }
}