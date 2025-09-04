using System;
using UnityEngine;

namespace OneM.LoadingSystem
{
    /// <summary>
    /// Interface used on objects able to be asynchronously loaded and unloaded.
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// Global event fired when any instance is loaded by any <see cref="ILoadable"/> implementation.
        /// </summary>
        public static event Action<GameObject> OnLoaded;

        /// Global event fired when any instance is unloaded from any <see cref="ILoadable"/> implementation.
        public static event Action<GameObject> OnUnloaded;

        /// <summary>
        /// Loads an instance asynchronously.
        /// </summary>
        /// <returns></returns>
        Awaitable LoadAsync();

        /// <summary>
        /// Unloads the previous instance (if any).
        /// </summary>
        void Unload();

        protected static void Load(GameObject instance) => OnLoaded?.Invoke(instance);
        protected static void Unload(GameObject instance) => OnUnloaded?.Invoke(instance);
    }
}