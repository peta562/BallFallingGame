using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Loaders {
    public sealed class AssetLoader {
        public async Task<T> LoadAsset<T>(string id, Transform transform = null) {
            var handle = Addressables.InstantiateAsync(id, transform);

            var gameObject = await handle.Task;

            if ( gameObject.TryGetComponent(out T component) == false ) {
                throw new NullReferenceException($"Object of type {typeof(T)} is null on load");
            }
            
            return component;
        }

        public void UnloadAsset(GameObject gameObject) {
            if ( gameObject == null ) {
                return;
            }
            
            Addressables.ReleaseInstance(gameObject);
        }
    }
}