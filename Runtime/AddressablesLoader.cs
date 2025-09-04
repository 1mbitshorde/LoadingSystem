using UnityEngine;
using UnityEngine.AddressableAssets;

namespace OneM.LoadingSystem
{
    /// <summary>
    /// Loads the <see cref="prefab"/> inside <see cref="place"/>.
    /// <para>You need a local Trigger Collider on this GameObject.</para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class AddressablesLoader : MonoBehaviour, ILoadable
    {
        [SerializeField, Tooltip("The addressable prefab to load")]
        private AssetReferenceGameObject prefab;
        [SerializeField, Tooltip("The place where your prefab will be loaded")]
        private Transform place;

        private GameObject instance;

        private void Reset()
        {
            place = transform;
            Loader.CheckTriggerCollider(gameObject);
        }

        public async Awaitable LoadAsync()
        {
            var operation = Addressables.InstantiateAsync(prefab, place);
            instance = await operation.Task;
            ILoadable.Load(instance);
        }

        public void Unload()
        {
            if (instance == null) return;

            ILoadable.Unload(instance);
            Addressables.ReleaseInstance(instance);
        }
    }
}