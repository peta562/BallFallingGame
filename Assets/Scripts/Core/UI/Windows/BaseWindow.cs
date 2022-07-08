using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Windows {
    public abstract class BaseWindow : MonoBehaviour {
        [SerializeField] Button CloseButton;

        Action _onHideAction;
        
        public void Init(Action onHideAction) {
            _onHideAction = onHideAction;
        }

        public void DeInit() {
            _onHideAction = null;
        }
        
        public virtual void Show() {
            CloseButton.onClick.AddListener(Hide);
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            CloseButton.onClick.RemoveListener(Hide);
            gameObject.SetActive(false);
            
            _onHideAction?.Invoke();
        }
    }
}