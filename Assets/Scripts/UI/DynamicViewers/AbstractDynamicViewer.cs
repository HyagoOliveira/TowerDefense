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
        [SerializeField] protected string spriteName;
        [SerializeField] protected string format = "D2";

        private void Reset() => textMesh = GetComponent<TMP_Text>();
        private void OnEnable() => GetValue().OnChanged += HandleValueChanged;
        private void OnDisable() => GetValue().OnChanged -= HandleValueChanged;

        private void HandleValueChanged(int value)
        {
            var result = value.ToString(format);
            var textWithSprite = $"<sprite=\"Icons\" name=\"{spriteName}\"> {result}";

            textMesh.text = textWithSprite;
        }

        protected abstract DynamicInteger GetValue();
    }
}