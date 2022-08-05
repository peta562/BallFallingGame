using System;
using Random = UnityEngine.Random;

namespace Utils {
    [Serializable]
    public class IntRange {
        public int Min;
        public int Max;

        public IntRange(int min, int max) {
            Min = min;
            Max = max;
        }

        public int GetRandomInt => Random.Range(Min, Max);
    }
}