using UnityEngine;
using TowerDefense.Physics;
using TowerDefense.Gameplay;
using System;

namespace TowerDefense.Managers
{
    [CreateAssetMenu(fileName = "MatchSettings", menuName = "Tower Defense/Match Settings", order = 110)]
    public sealed class MatchSettings : ScriptableObject
    {
        [SerializeField] private int initialHealth = 100;
        [SerializeField] private int initialCurrency = 500;

        [field: Space]
        [field: SerializeField] public DefenderTower[] Defenders { get; private set; }

        public event Action OnStarted;

        public TowerPlacer Placer { get; private set; }

        public DynamicValue<int> Round { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Score { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Health { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Currency { get; private set; } = new DynamicValue<int>();

        internal void Initialize(TowerPlacer placer) => Placer = placer;

        internal void Start()
        {
            ResetValues();
            OnStarted?.Invoke();
        }

        private void ResetValues()
        {
            Round.Value = 0;
            Score.Value = 0;
            Health.Value = initialHealth;
            Currency.Value = initialCurrency;
        }

        public void TrySpawnDefender(int index)
        {
            var defender = Defenders[index];
            var price = defender.Price;
            var canPurchase = Currency.Value >= price;

            if (!canPurchase) return;

            var instance = Instantiate(defender);
            Placer.SetPassenger(instance);

            Currency.Value -= price;
        }
    }
}