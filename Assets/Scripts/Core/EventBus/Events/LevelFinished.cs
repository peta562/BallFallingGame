using UnityEngine;

namespace Core.EventBus.Events {
    public readonly struct LevelFinished {
        public readonly bool Win;

        public LevelFinished(bool win) {
            Win = win;
        }
    }
}