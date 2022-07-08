using System.Collections.Generic;

namespace Core.UI.Windows {
    public sealed class WindowManager {
        readonly Stack<BaseWindow> _activeWindows = new Stack<BaseWindow>();
        
        List<BaseWindow> _windows;
        BaseWindow _currentWindow;
        
        public void Init(List<BaseWindow> windows) {
            _windows = windows;

            foreach (var window in _windows) {
                window.Init(TryShowLast);
            }
        }

        public void DeInit() {
            foreach (var window in _windows) {
                window.DeInit();
            }
            
            _windows = null;
        }

        public void ShowWindow<T>(bool remember = true) where T : BaseWindow {
            foreach (var window in _windows) {
                if ( window is T tWindow ) {
                    if ( _currentWindow != null ) {
                        if ( remember ) {
                            _activeWindows.Push(_currentWindow);
                        }

                        _currentWindow.Hide();
                    }

                    tWindow.Show();
                    _currentWindow = tWindow;
                    return;
                }
            }
        }

        void Show(BaseWindow window, bool remember = true) {
            if ( _currentWindow != null ) {
                if ( remember ) {
                    _activeWindows.Push(_currentWindow);
                }

                _currentWindow.Hide();
            }

            window.Show();
            _currentWindow = window;
        }

        void TryShowLast() {
            if ( _activeWindows.Count != 0 ) {
                Show(_activeWindows.Pop(), false);
            }
        }
    }
}