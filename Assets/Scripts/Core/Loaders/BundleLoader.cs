using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Loaders {
    public sealed class BundleLoader {
        AsyncOperationHandle<IList<GameObject>> _loadHandle;
        
        public async Task<IEnumerable<GameObject>> DownloadBundle(string bundleName) {
            _loadHandle = Addressables.LoadAssetsAsync<GameObject>(new List<string> {bundleName},
                asset => { }, 
                Addressables.MergeMode.None, 
                false);

            var result = await _loadHandle.Task;

            return result;
        }
        
        public void ReleaseBundle() {
            Addressables.Release(_loadHandle);
        }
    }
}