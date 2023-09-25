using UnityEngine;
using TowerDefense.VisualEffects;

namespace TowerDefense.Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MaterialChanger))]
    public sealed class Defender : MonoBehaviour
    {
        [field: SerializeField] public MaterialChanger MaterialChanger { get; private set; }

        private void Reset()
        {
            MaterialChanger = GetComponent<MaterialChanger>();
        }
    }
}