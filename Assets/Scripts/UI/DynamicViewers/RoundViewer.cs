using UnityEngine;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    public sealed class RoundViewer : AbstractDynamicViewer
    {
        protected override DynamicValue<int> GetValue() => settings.Round;
    }
}