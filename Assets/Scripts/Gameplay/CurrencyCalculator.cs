using UnityEngine;

namespace TowerDefense.Gameplay
{
    public sealed class CurrencyCalculator
    {
        private readonly DynamicValue<int> currency;

        public CurrencyCalculator(DynamicValue<int> currency)
        {
            this.currency = currency;
        }

        public bool CanPurchase(int price) => currency.Value >= price;
        public bool CanPurchase(DefenderTower tower) => CanPurchase(tower.PurchasePrice);
        public bool CanUpgrade(DefenderTower tower) => CanPurchase(tower.UpgradePrice);

        public void TryPurchase(DefenderTower tower)
        {
            if (CanPurchase(tower)) Purchase(tower);
        }

        public void Purchase(DefenderTower tower)
        {
            var value = currency.Value - tower.PurchasePrice;
            currency.Value = Mathf.Max(0, value);

            // Add Analytics here if a valid project
        }
    }
}