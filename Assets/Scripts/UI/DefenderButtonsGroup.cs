using UnityEngine;
using TowerDefense.Managers;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    public sealed class DefenderButtonsGroup : MonoBehaviour
    {
        [SerializeField] private DefenderButton prefab;
        [SerializeField] private MatchSettings settings;

        private DefenderButton[] buttons = new DefenderButton[0];

        private void OnEnable() => settings.OnStarted += HandleMatchStarted;
        private void OnDisable() => settings.OnStarted -= HandleMatchStarted;

        private void HandleMatchStarted() => ResetButtons();

        private void ResetButtons()
        {
            Clear();
            Instantiate();
        }

        private void Instantiate()
        {
            buttons = new DefenderButton[settings.Defenders.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = Instantiate(prefab);
                buttons[i].Initialize(i, transform);
            }
        }

        private void Clear()
        {
            foreach (var button in buttons)
            {
                Destroy(button.gameObject);
            }

            buttons = new DefenderButton[0];
        }
    }
}