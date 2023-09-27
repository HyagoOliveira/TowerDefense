using TMPro;
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

        [Space]
        [SerializeField] private TMP_Text defenderName;
        [SerializeField] private TMP_Text defenderPrice;

        private int defenderIndex;

        private void Reset() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(HandleButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(HandleButtonClicked);

        internal void Initialize(int defenderIndex, Transform parent)
        {
            this.defenderIndex = defenderIndex;

            transform.SetParent(parent);
            transform.localScale = Vector3.one;

            var defender = settings.Defenders[defenderIndex];

            defenderName.text = defender.Name;
            defenderPrice.text = defender.Price.ToString("D2");
        }

        private void HandleButtonClicked() => settings.SpawnDefender(defenderIndex);
    }
}