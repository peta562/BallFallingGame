using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Windows {
    public abstract class BaseWindow : MonoBehaviour {
        [SerializeField] Button CloseButton;
        [SerializeField] ShowAnimationType ShowAnimation;
        [SerializeField] HideAnimationType HideAnimation;

        RectTransform _rectTransform;
        CanvasGroup _canvasGroup;
        Action _onHideAction;
        Sequence _sequence;

        public void Init(Action onHideAction) {
	        _rectTransform = GetComponent<RectTransform>();
	        _canvasGroup = GetComponent<CanvasGroup>();
	        
            _onHideAction = onHideAction;
        }

        public void DeInit() {
	        _rectTransform = null;
	        _canvasGroup = null;
	        
            _onHideAction = null;
        }
        
        public virtual void Show() {
            CloseButton.onClick.AddListener(Hide);
            gameObject.SetActive(true);
            PlayShowAnimation();
        }

        public virtual void Hide() {
            CloseButton.onClick.RemoveListener(Hide);
            PlayHideAnimation();
            gameObject.SetActive(false);
            _onHideAction?.Invoke();
        }

        void PlayShowAnimation() {
	        _sequence.Kill();
	        _sequence = GetShowAnimation();
	        _sequence.Play();
        }

        void PlayHideAnimation() {
	        _sequence.Kill();
	        _sequence = GetHideAnimation();
	        _sequence.Play();
        }

        #region Show Animation

        const float ShowFadeTime       = 0.255f;
        const float ShowMoveTime       = 0.25f;
        const float ShowRotateTime     = 0.5f;
        const float ShowOffset         = 3000;
        const float ShowShakePosTime   = 0.35f;
        const float ShowShakeScaleTime = 1.5f;
        const float ShowScaleDown      = 0.015f;
        const float ShakeForce         = 200;
        const float ShowScaleUpTime    = 0.6f;

        Sequence GetShowAnimation() {
            switch ( ShowAnimation ) {
                case ShowAnimationType.ScaleUp: {
                    return GetShowSequence_Scaled();
                }
                case ShowAnimationType.FromDown: {
                    return GetShowSequence_From(Vector2.down);
                }
                case ShowAnimationType.FromTop: {
                    return GetShowSequence_From(Vector2.up);
                }
                case ShowAnimationType.FromLeft: {
                    return GetShowSequence_From(Vector2.left);
                }
                case ShowAnimationType.FromRight: {
                    return GetShowSequence_From(Vector2.right);
                }
                case ShowAnimationType.RotateScaleUp: {
                    return GetShowSequence_RotateScaled();
                }
                default: {
                    _canvasGroup.alpha              = 1;
                    _rectTransform.localScale       = Vector3.one;
                    _rectTransform.anchoredPosition = Vector3.zero;
                    return DOTween.Sequence();
                }
            }
        }
        
        Sequence GetShowSequence_Scaled() {
			_canvasGroup.alpha              = 0;
			_rectTransform.localScale       = Vector3.one * 0.5f;
			_rectTransform.anchoredPosition = Vector3.zero;
			var sequence = DOTween.Sequence();
			sequence.Append(_canvasGroup.DOFade(1, ShowFadeTime));
			sequence.Insert(0f, _rectTransform.DOScale(1, ShowScaleUpTime).SetEase(Ease.OutElastic, 1.2f, 0));
			return sequence;
		}

		Sequence GetShowSequence_From(Vector2 dir) {
			_canvasGroup.alpha              = 0;
			_rectTransform.localScale       = Vector3.one;
			_rectTransform.anchoredPosition = dir * ShowOffset;
			var sequence = DOTween.Sequence();
			sequence.Append(_canvasGroup.DOFade(1, ShowFadeTime));
			sequence.Insert(0, _rectTransform.DOAnchorPos(Vector3.zero, ShowMoveTime));
			sequence.Append(_rectTransform.DOShakeAnchorPos(ShowShakePosTime, -dir * ShakeForce, 5, 0, true));
			sequence.Append(_rectTransform.DOPunchScale(dir * ShowScaleDown, ShowShakeScaleTime, 5).SetEase(Ease.InOutBounce));
			return sequence;
		}

		Sequence GetShowSequence_RotateScaled() {
			_canvasGroup.alpha              = 1;
			_rectTransform.localScale       = Vector3.one;
			_rectTransform.anchoredPosition = Vector3.zero;
			var sequence = DOTween.Sequence();
			sequence.Append(_rectTransform.DOScale(Vector3.one, ShowRotateTime));
			sequence.Join(_rectTransform.DORotate(Vector3.forward * 360, ShowRotateTime, RotateMode.FastBeyond360));
			return sequence;
		}
		
		#endregion

		#region Hide Animation

		const float HideFadeDelay     = 0.1F;
		const float HideFadeTime      = 0.19F;
		const float HideScaleUp       = 1.2f;
		const float HideScaleUpTime   = 0.05F;
		const float HideScaleDownTime = 0.255F;
		const float HideMoveFadeTime  = 1.5f;
		const float HideOffset        = 3000;

		Sequence GetHideAnimation() {
			switch ( HideAnimation ) {
				case HideAnimationType.ScaleDown: {
					return GetHideSequence_Scaled();
				}
				case HideAnimationType.ToDown: {
					return GetHideSequence_To(Vector2.down);
				}
				case HideAnimationType.ToTop: {
					return GetHideSequence_To(Vector2.up);
				}
				case HideAnimationType.ToLeft: {
					return GetHideSequence_To(Vector2.left);
				}
				case HideAnimationType.ToRight: {
					return GetHideSequence_To(Vector2.right);
				}
				default: {
					return DOTween.Sequence();;
				}
			}
		}

		Sequence GetHideSequence_Scaled() {
			var sequence = DOTween.Sequence();;
			sequence.AppendInterval(HideFadeDelay);
			sequence.Append(_canvasGroup.DOFade(0, HideFadeTime));
			sequence.Insert(0, _rectTransform.DOScale(HideScaleUp, HideScaleUpTime));
			sequence.Insert(HideScaleUpTime, _rectTransform.DOScale(0, HideScaleDownTime));
			return sequence;
		}

		Sequence GetHideSequence_To(Vector2 dir) {
			_rectTransform.localScale = Vector3.one;
			var nextAnchoredPosition = dir * HideOffset;
			var sequence             = DOTween.Sequence();;
			sequence.Append(_canvasGroup.DOFade(0, HideMoveFadeTime));
			sequence.Insert(0, _rectTransform.DOAnchorPos(nextAnchoredPosition, ShowMoveTime));
			return sequence;
		}
		
		#endregion
    }
}