using System;

namespace TowerDefense
{
    public sealed class DynamicValue<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Event fired when the current <see cref="Value"/> is changed.
        /// </summary>
        public event Action<T> OnChanged;

        /// <summary>
        /// The current value.
        /// </summary>
        public T Value
        {
            get => value;
            internal set
            {
                if (this.value.Equals(value)) return;

                this.value = value;
                OnChanged?.Invoke(this.value);
            }
        }

        private T value;
    }
}