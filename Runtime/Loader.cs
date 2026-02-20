using UnityEngine;
using System.Collections.Generic;

namespace OneM.LoadingSystem
{
    /// <summary>
    /// Loads <see cref="ILoadable"/> implementations while inside local collider area.
    /// Put this component inside your Player or Camera.
    /// <para>You need a local Trigger Collider on this GameObject.</para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Loader : MonoBehaviour
    {
        [SerializeField, Tooltip("Only ILoadable implementations inside this mask will be used.")]
        private LayerMask collisions;

        public const int MAX_COLLISIONS = 30;
        private readonly HashSet<ILoadable> loadables = new(MAX_COLLISIONS);

        private void Reset() => CheckTriggerCollider(gameObject);
        private void OnTriggerEnter(Collider other) => TryAddLoadable(other);
        private void OnTriggerExit(Collider other) => TryRemoveLoadbale(other);

        private async void TryAddLoadable(Collider collider)
        {
            if (!IsInsideCollisions(collider.gameObject.layer)) return;

            var hasLoadable = collider.TryGetComponent(out ILoadable loadable);
            if (!hasLoadable || !loadable.CanLoad() || loadables.Contains(loadable)) return;

            await loadable.LoadAsync();
            loadables.Add(loadable);
        }

        private void TryRemoveLoadbale(Collider collider)
        {
            if (!IsInsideCollisions(collider.gameObject.layer)) return;

            var hasLoadable = collider.TryGetComponent(out ILoadable loadable);
            if (!hasLoadable || loadable.IsLoading) return;

            var wasUnloaded = loadable.Unload();
            if (wasUnloaded) loadables.Remove(loadable);
        }

        internal static void CheckTriggerCollider(GameObject instance)
        {
            var collider = instance.GetComponent<Collider>();
            var isInvalid = collider == null || !collider.isTrigger;
            if (isInvalid) Debug.LogWarning($"{instance.name} should have a Collider with Is Trigger enabled.");
        }

        private bool IsInsideCollisions(int layer) => IsInsideLayerMask(collisions, layer);
        private static bool IsInsideLayerMask(LayerMask mask, int layer) => ((1 << layer) & mask.value) != 0;
    }
}