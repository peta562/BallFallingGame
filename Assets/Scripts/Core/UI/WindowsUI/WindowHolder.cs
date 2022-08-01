using System.Collections.Generic;
using UnityEngine;

namespace Core.UI.WindowsUI {
    public class WindowHolder : MonoBehaviour {
        public List<BaseWindow> Windows = new List<BaseWindow>();
        public WindowBackground WindowBackground;
    }
}