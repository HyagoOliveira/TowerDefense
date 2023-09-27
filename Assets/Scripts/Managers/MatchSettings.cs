using System;
using UnityEngine;
using TowerDefense.Physics;
using TowerDefense.Gameplay;

namespace TowerDefense.Managers
{
    [CreateAssetMenu(fileName = "MatchSettings", menuName = "Tower Defense/Match Settings", order = 110)]
    public sealed class MatchSettings : ScriptableObject
    {
        [SerializeField] private int initialHealth = 100;
        [SerializeField] private int initialCurrency = 500;
        [SerializeField] private DefenderTower[] towers = new DefenderTower[0];

        public event Action OnStarted;

        public int TowerSize => towers.Length;

        public TowerPlacer Placer { get; private set; }
        public CurrencyCalculator Calculator { get; private set; }

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

        public void TrySpawnTower(int index)
        {
            var tower = GetTower(index);
            if (!Calculator.CanPurchase(tower)) return;

            Calculator.Purchase(tower);

            var instance = Instantiate(tower);
            Placer.SetTower(instance);
        }

        public DefenderTower GetTower(int index) => towers[index];

        private void ResetValues()
        {
            Round.Value = 0;
            Score.Value = 0;
            Health.Value = initialHealth;
            Currency.Value = initialCurrency;

            Calculator = new CurrencyCalculator(Currency);
        }
    }
}