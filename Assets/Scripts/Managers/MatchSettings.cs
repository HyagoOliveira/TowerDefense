using UnityEngine;

namespace TowerDefense.Managers
{
    [CreateAssetMenu(fileName = "MatchSettings", menuName = "Tower Defense/Match Settings", order = 110)]
    public sealed class MatchSettings : ScriptableObject
    {
        [SerializeField] private int initialScore = 0;
        [SerializeField] private int initialHealth = 100;
        [SerializeField] private int initialCurrency = 500;

        public DynamicValue<int> Score { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Health { get; private set; } = new DynamicValue<int>();
        public DynamicValue<int> Currency { get; private set; } = new DynamicValue<int>();

        internal void ResetValues()
        {
            Score.Value = initialScore;
            Health.Value = initialHealth;
            Currency.Value = initialCurrency;
        }
    }
}