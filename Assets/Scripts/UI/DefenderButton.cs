using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Managers;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class DefenderButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private MatchSettings settings;

        private int defenderIndex;

        private void Reset() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(HandleButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(HandleButtonClicked);

        internal void Initialize(int defenderIndex, Transform parent)
        {
            this.defenderIndex = defenderIndex;
            transform.SetParent(parent);
        }

        private void HandleButtonClicked() => settings.SpawnDefender(defenderIndex);
    }
}