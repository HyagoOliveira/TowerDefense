using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Physics;
using TowerDefense.Gameplay;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class DefenderButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Defender defender;

        private void Reset() => button = GetComponent<Button>();
        private void OnEnable() => button.onClick.AddListener(HandleButtonClicked);
        private void OnDisable() => button.onClick.RemoveListener(HandleButtonClicked);

        private void HandleButtonClicked() => SpawnDefender();

        //TODO move code elsewhere
        private void SpawnDefender()
        {
            var placer = FindObjectOfType<MousePlacer>();
            var instance = Instantiate(defender);

            placer.SetPassenger(instance);
        }
    }
}