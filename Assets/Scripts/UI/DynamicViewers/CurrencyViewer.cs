using UnityEngine;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    public sealed class CurrencyViewer : AbstractDynamicViewer
    {
        protected override DynamicValue<int> GetValue() => settings.Currency;
    }
}