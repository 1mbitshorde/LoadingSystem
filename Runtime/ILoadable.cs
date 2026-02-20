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
        /// Wether is currently loading an instance or not.
        /// </summary>
        bool IsLoading { get; }

        /// <summary>
        /// Whether can load a new instance or not.
        /// </summary>
        /// <returns>False if it's currently loading an instance.</returns>
        bool CanLoad() => !IsLoading;

        /// <summary>
        /// Loads an instance asynchronously.
        /// </summary>
        /// <returns></returns>
        Awaitable LoadAsync();

        /// <summary>
        /// Tries to unload the previous instance (if any).
        /// </summary>
        /// <returns>Whether could unload the previous instance.</returns>
        bool Unload();

        protected static void Load(GameObject instance) => OnLoaded?.Invoke(instance);
        protected static void Unload(GameObject instance) => OnUnloaded?.Invoke(instance);
    }
}