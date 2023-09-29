using UnityEngine;
using TowerDefense.Managers;

namespace TowerDefense.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public sealed class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MatchSettings settings;

        private void Reset() => canvas = GetComponent<Canvas>();
        private void OnEnable() => settings.OnGameOver += HandleGameOver;
        private void OnDisable() => settings.OnGameOver -= HandleGameOver;

        private void HandleGameOver() => canvas.enabled = true;
    }
}