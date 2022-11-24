using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Presenter;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CameraRotateView : MonoBehaviour {
        // Dependencies
        [Inject]
        private IGamePresenter gamePresenter;

        // Private properties
        private Tweener tween;

        // Unity methods
        private void Start() {
            gamePresenter.GameState
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnGameStateChange);
        }

        // Private Methods
        private void OnGameStateChange(GameSate gameSate) {
            if (gameSate == GameSate.Complete) {
                Rotate();
            }
            else {
                Reset();
            }
        }

        private void Rotate() {
            tween = transform.DORotate(new Vector3(0, -360, 0), 10, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental);
        }

        private void Reset() {
            tween?.Kill();
            tween = null;
            transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
    }
}
