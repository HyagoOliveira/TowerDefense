using UnityEngine;
using TowerDefense.VisualEffects;

namespace TowerDefense.Physics
{
    /// <summary>
    /// Interface for objects able to be a passenger.
    /// </summary>
    public interface IPassengerable
    {
        public Transform transform { get; }
        public MaterialChanger MaterialChanger { get; }

        public bool CanPlace();
    }
}