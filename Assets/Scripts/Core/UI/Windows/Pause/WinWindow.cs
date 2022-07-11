using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Windows.Pause {
    public sealed class WinWindow : BaseWindow {
        [SerializeField] Button OkButton;

        public override void Show() {
            OkButton.onClick.AddListener(Hide);

            base.Show();
        }

        public override void Hide() {
            OkButton.onClick.RemoveListener(Hide);

            base.Hide();
        }
    }
}