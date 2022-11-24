using System;
using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Presentation.View.Util.InputHelper;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class ClickControllerView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private PlatformView platformView;

        // Dependencies
        [Inject]
        private IClickEvent clickEvent;
        [Inject]
        private IGamePresenter gamePresenter;

        private bool isPlaying;

        // Unity Methods
        private void Awake() {
            gamePresenter.GameState
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnGameStateChange);

            clickEvent.Click
                .Where(input => {
                    bool isOverUI = false;
                    if (input.state == InputState.Begin) {
                        isOverUI = Pointer.IsPointerOverUIObject(input.position);
                    }
                    return !isOverUI && isPlaying;
                })
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnClick);
        }

        // Private Methods
        private void OnClick(InputData input) {
            platformView.OnClick();
        }

        private void OnGameStateChange(GameSate gameSate) {
            if (gameSate == GameSate.Playing) {
                /*
                 * Hack: When a close button is cliked and popup closes, 
                 * Pointer.IsPointerOverUIObject returns false.
                 * So we dalay input and discard this click.
                 */
                Observable.Empty<object>()
                    .DelayFrame(1)
                    .DoOnCompleted(() => {
                        isPlaying = true;
                    })
                    .Subscribe();
              
            }
            else {
                isPlaying = false;
            }
        }
    }
}
