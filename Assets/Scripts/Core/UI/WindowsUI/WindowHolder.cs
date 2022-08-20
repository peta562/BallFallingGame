using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Loaders;
using UnityEngine;

namespace Core.UI.WindowsUI {
    public class WindowHolder : Singleton<WindowHolder> {
        [SerializeField] WindowsRoot WindowsRoot;

        readonly Dictionary<string, List<BaseWindow>> _windows = new Dictionary<string, List<BaseWindow>>();

        WindowBackground _windowBackground;

        BundleLoader BundleLoader => GameContext.Instance.BundleLoader;
        PrefabLoader PrefabLoader => GameContext.Instance.PrefabLoader;

        public async Task<WindowBackground> GetWindowBackground() {
            if ( _windowBackground ) {
                return _windowBackground;
            }
            _windowBackground = await PrefabLoader.LoadAsset<WindowBackground>("WindowBackground", WindowsRoot.transform);
            
            return _windowBackground;
        }

        public async Task<List<BaseWindow>> GetWindows(string bundleName) {
            if ( _windows.TryGetValue(bundleName, out var windows) ) {
                return windows;
            }
            
            var downloadedWindows = await BundleLoader.DownloadBundle(bundleName);
            var instantiatedWindows = InstantiateWindows(downloadedWindows);
            _windows.Add(bundleName, instantiatedWindows);

            return _windows[bundleName];
        }

        List<BaseWindow> InstantiateWindows(IEnumerable<GameObject> windows) {
            var addedWindows = new List<BaseWindow>();
            
            foreach (var window in windows) {
                if ( !Instantiate(window, WindowsRoot.transform)
                    .TryGetComponent<BaseWindow>(out var instantiatedWindow) ) {
                    Debug.LogError($"There are no component {typeof(BaseWindow)} on window.");
                    continue;
                };
                instantiatedWindow.gameObject.SetActive(false);
                addedWindows.Add(instantiatedWindow);
            }

            return addedWindows;
        }
    }
}