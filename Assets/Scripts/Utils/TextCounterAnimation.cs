using System.Collections;
using TMPro;
using UnityEngine;

namespace Utils {
    public class TextCounterAnimation : MonoBehaviour {
        [SerializeField] TMP_Text Text;
        [SerializeField] int CountFPS = 30;
        [SerializeField] float Duration = 1f;

        int _value;

        public int Value {
            get => _value;
            set {
                UpdateText(value);
                _value = value;
            }
        }

        Coroutine CountingCoroutine;

        private void UpdateText(int newValue) {
            if ( CountingCoroutine != null ) {
                StopCoroutine(CountingCoroutine);
            }

            CountingCoroutine = StartCoroutine(CountText(newValue));
        }

        private IEnumerator CountText(int newValue) {
            WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
            var previousValue = _value;
            int stepAmount;

            if ( newValue - previousValue < 0 ) {
                stepAmount =
                    Mathf.FloorToInt((newValue - previousValue) /
                                     (CountFPS *
                                      Duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
            } else {
                stepAmount =
                    Mathf.CeilToInt((newValue - previousValue) /
                                    (CountFPS *
                                     Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
            }

            if ( previousValue < newValue ) {
                while ( previousValue < newValue ) {
                    previousValue += stepAmount;
                    if ( previousValue > newValue ) {
                        previousValue = newValue;
                    }

                    Text.text = previousValue.ToString();

                    yield return Wait;
                }
            } else {
                while ( previousValue > newValue ) {
                    previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                    if ( previousValue < newValue ) {
                        previousValue = newValue;
                    }

                    Text.text = previousValue.ToString();

                    yield return Wait;
                }
            }
        }
    }
}