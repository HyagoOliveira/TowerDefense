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
        [SerializeField] private EnemyWave[] enemyWaves = new EnemyWave[0];

        public event Action OnStarted;
        public event Action<EnemyWave> OnEnemyWaveSpawned;

        public int Towers => towers.Length;
        public int EnemyWaves => enemyWaves.Length;

        public TowerPlacer Placer { get; private set; }
        public CurrencyCalculator Calculator { get; private set; }

        public DynamicValue<int> Round { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Score { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Health { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Currency { get; private set; } = new DynamicValue<int>();

        private int currentEnemyWaveIndex;

        internal void Initialize(TowerPlacer placer) => Placer = placer;
        internal void Enable() => Placer.OnTowerPlaced += HandleTowerPlaced;
        internal void Disable() => Placer.OnTowerPlaced -= HandleTowerPlaced;

        internal void Start()
        {
            ResetValues();
            OnStarted?.Invoke();
        }

        public void TrySpawnTower(int index)
        {
            var tower = GetTower(index);
            if (!Calculator.CanPurchase(tower)) return;

            var instance = Instantiate(tower);
            Placer.SetTower(instance);
        }

        public void SpawnNextEnemyWave()
        {
            currentEnemyWaveIndex++;

            if (currentEnemyWaveIndex >= EnemyWaves)
            {
                currentEnemyWaveIndex = EnemyWaves - 1;
                GetEnemyWave(currentEnemyWaveIndex).AdditionalQuantity++;
            }

            var wave = GetEnemyWave(currentEnemyWaveIndex);
            OnEnemyWaveSpawned?.Invoke(wave);
        }

        public DefenderTower GetTower(int index) => towers[index];
        public EnemyWave GetEnemyWave(int index) => enemyWaves[index];

        private void HandleTowerPlaced(DefenderTower tower) => Calculator.Purchase(tower);

        private void ResetValues()
        {
            Round.Value = 0;
            Score.Value = 0;
            Health.Value = initialHealth;
            Currency.Value = initialCurrency;
            currentEnemyWaveIndex = -1;

            Calculator = new CurrencyCalculator(Currency);
        }
    }
}