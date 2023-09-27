using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Gameplay;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public sealed class EnergyBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private Damageable damageable;

        private void Reset() => slider = GetComponent<Slider>();
        private void Awake() => FindParentDamageable();
        private void OnEnable() => damageable.Energy.OnChanged += HandleEnergyChanged;
        private void OnDisable() => damageable.Energy.OnChanged -= HandleEnergyChanged;

        private void HandleEnergyChanged(int energy) => slider.value = energy;

        private void FindParentDamageable() => SetDamageable(GetComponentInParent<Damageable>());

        private void SetDamageable(Damageable damageable)
        {
            this.damageable = damageable;
            slider.minValue = 0;
            slider.maxValue = damageable.Energy.Value;
            slider.value = damageable.Energy.Value;
        }
    }
}