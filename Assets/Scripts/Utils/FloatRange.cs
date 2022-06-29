using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils {
    [Serializable]
    public class FloatRange {
        public float Min;
        public float Max;

        public FloatRange(float min, float max) {
            Min = min;
            Max = max;
        }

        public float GetRandomFloat => Random.Range(Min, Max);
    }
}