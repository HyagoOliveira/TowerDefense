using UnityEngine;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    public sealed class HealthViewer : AbstractDynamicViewer
    {
        protected override DynamicValue<int> GetValue() => settings.Health;
    }
}