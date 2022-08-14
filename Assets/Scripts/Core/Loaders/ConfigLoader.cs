using System;
using System.Threading.Tasks;
using Configs;
using UnityEngine.AddressableAssets;

namespace Core.Loaders {
    public sealed class ConfigLoader : IConfigLoader {
        public async Task<T> Load<T>(string id) {
            var handle = Addressables.LoadAssetAsync<T>(id);

            var config = await handle.Task;
            
            if ( config == null ) {
                throw new NullReferenceException($"Object of type {typeof(T)} is null on load");
            }
            
            return config;
        }

        public void Unload(BaseConfig config) {
            if ( config == null ) {
                return;
            }
            
            Addressables.Release(config);
        }
    }
}