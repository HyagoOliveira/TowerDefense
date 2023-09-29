using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Gameplay;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    public sealed class UpgradeTowerPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text currentWeaponsCounter;
        [SerializeField] private TMP_Text upgradedWeaponsCounter;

        [Space]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button upgradeButton;

        public event Action OnClosed;

        private DefenderTower tower;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(HandleCloseButtonClicked);
            upgradeButton.onClick.AddListener(HandleUpgradeButtonClicked);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(HandleCloseButtonClicked);
            upgradeButton.onClick.RemoveListener(HandleUpgradeButtonClicked);
        }

        public void Open(DefenderTower tower)
        {
            this.tower = tower;
            ShowTowerAttributes();
        }

        public void Close() => OnClosed?.Invoke();
        public void TryUpgrade() { }

        private void HandleCloseButtonClicked() => Close();
        private void HandleUpgradeButtonClicked() => TryUpgrade();

        private void ShowTowerAttributes()
        {
            title.text = tower.DysplayName;
        }
    }
}