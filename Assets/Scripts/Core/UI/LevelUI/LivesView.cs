using TMPro;
using UnityEngine;

namespace Core.UI.LevelUI {
    public sealed class LivesView : MonoBehaviour {
        [SerializeField] TMP_Text LivesText;
        
        public void Init(int lives) {
            LivesText.text = lives.ToString();
        }
        
        public void UpdateLives(int lives) {
            LivesText.text = lives.ToString();
        }
    }
}