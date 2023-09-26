using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "MatchSettings", menuName = "Tower Defense/Match Settings", order = 110)]
    public sealed class MatchSettings : ScriptableObject
    {
        [SerializeField] private int initialScore = 0;
        [SerializeField] private int initialHealth = 100;
        [SerializeField] private int initialCurrency = 500;

        public DynamicValue<int> Score { get; private set; }
        public DynamicValue<int> Health { get; private set; }
        public DynamicValue<int> Currency { get; private set; }

        public void Initialize() => ResetValues();

        private void ResetValues()
        {
            Score = new DynamicValue<int>(initialScore);
            Health = new DynamicValue<int>(initialHealth);
            Currency = new DynamicValue<int>(initialCurrency);
        }
    }
}