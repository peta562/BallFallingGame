using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI.Windows {
    public sealed class WindowManager {
        readonly Stack<BaseWindow> _activeWindows = new Stack<BaseWindow>();
        
        List<BaseWindow> _windows;
        WindowBackground _windowBackground;
        BaseWindow _currentWindow;
        
        public void Init(List<BaseWindow> windows, WindowBackground windowBackground) {
            _windows = windows;
            _windowBackground = windowBackground;

            foreach (var window in _windows) {
                window.Init(OnHide);
            }
        }

        public void DeInit() {
            foreach (var window in _windows) {
                window.DeInit();
            }
            
            _windows = null;
            _windowBackground = null;
        }

        public void ShowWindow<T>(Action<T> action = null, bool remember = true) where T : BaseWindow {
            foreach (var window in _windows) {
                if ( window is T tWindow ) {
                    if ( _currentWindow != null ) {
                        if ( remember ) {
                            _activeWindows.Push(_currentWindow);
                        }

                        _currentWindow.Hide();
                    }

                    _windowBackground.Show();
                    
                    if ( action != null ) {
                        var component = tWindow.GetComponent<T>();
                        if ( component != null ) {
                            action.Invoke(component);
                        }
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

            _windowBackground.Show();
            window.Show();
            _currentWindow = window;
        }

        void OnHide() {
            _windowBackground.Hide();
            TryShowLast();
        }

        void TryShowLast() {
            if ( _activeWindows.Count != 0 ) {
                Show(_activeWindows.Pop(), false);
            } else {
                _currentWindow = null;
            }
        }
    }
}