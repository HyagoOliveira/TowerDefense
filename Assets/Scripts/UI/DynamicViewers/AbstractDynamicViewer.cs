using TMPro;
using UnityEngine;
using TowerDefense.Managers;

namespace TowerDefense.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class AbstractDynamicViewer : MonoBehaviour
    {
        [SerializeField] protected TMP_Text textMesh;
        [SerializeField] protected MatchSettings settings;
        [SerializeField] protected string format = "D2";

        private void Reset() => textMesh = GetComponent<TMP_Text>();
        private void OnEnable() => GetValue().OnChanged += HandleValueChanged;
        private void OnDisable() => GetValue().OnChanged -= HandleValueChanged;

        private void HandleValueChanged(int value) => textMesh.text = value.ToString(format);

        protected abstract DynamicValue<int> GetValue();
    }
}