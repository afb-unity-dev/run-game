using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Interactor;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Util;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class PlatformView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private CubeSpawner cubeSpawner;
        [SerializeField]
        private FinishSpawner finishSpawner;
        [SerializeField]
        private CutSpawner cutSpawner;
        [SerializeField]
        private Transform movingPlatform;

        // Dependencies
        [Inject]
        private IPlatformInteractor platformInteractor;
        [Inject]
        private IPlatformPresenter platformPresenter;
        [Inject]
        private IGamePresenter gamePresenter;

        // Private Properties
        private CubeView currentCubeView;
        private int lastPosition = 0;

        // Unity Methods
        private void Start() {
            finishSpawner.Spawn(0);

            platformPresenter.CharacterPosition
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnCharacterPosition);

            platformPresenter.TargetPosition
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnTargetPosition);

            gamePresenter.GameState
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnGameStateChange);

            platformPresenter.OnResetPlatform
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnResetPlatform);
        }

        // Public Methods
        public void OnClick() {
            float xPosition = currentCubeView.GetXPosition();
            platformInteractor.PlaceCube(xPosition);
        }

        // Private Methods
        private void OnCharacterPosition(int position) {
            lastPosition = position;
            float platformPos = lastPosition * Constants.CUBE_LENGTH;
            MovePlatform(platformPos, MoveComplete);
        }

        private void OnTargetPosition(int position) {
            if (position == 0) {
                return;
            }

            float zPosition = position * Constants.CUBE_LENGTH
                + Constants.INITIAL_POSITION
                + Constants.FINISH_LEGHTH / 2;

            finishSpawner.Spawn(zPosition);
        }

        private void OnGameStateChange(GameSate gameSate) {
            if (gameSate == GameSate.Complete) {
                float platformPos = (lastPosition + 1) * Constants.CUBE_LENGTH
                    + Constants.INITIAL_POSITION
                    + Constants.FINISH_LEGHTH / 2;


                MovePlatform(platformPos);
            }
            else if (gameSate == GameSate.Fail) {
                float platformPos = lastPosition * Constants.CUBE_LENGTH
                    + Constants.FINISH_LEGHTH;

                MovePlatform(platformPos);
            }
        }

        private void OnResetPlatform(bool restart) {
            if (restart) {
                Restart();
            }
            else {
                Reset();
            }
        }

        private void MovePlatform(float platformPos, TweenCallback onComplete = null) {
            movingPlatform.DOMoveZ(-platformPos, 0.5f)
                .OnComplete(onComplete);
        }

        private void MoveComplete() {
            float zPosition = lastPosition * Constants.CUBE_LENGTH + Constants.INITIAL_POSITION + Constants.CUBE_LENGTH / 2;
            currentCubeView = cubeSpawner.Spawn(zPosition);
        }

        private void Restart() {
            movingPlatform.transform.DOMove(Vector3.zero, 0.5f);
            cubeSpawner.DespawnLast(lastPosition + 1);
            finishSpawner.DespawnLast();
            finishSpawner.Spawn(0);
        }

        private void Reset() {
            float zPos = movingPlatform.position.z;
            movingPlatform.transform.position = Vector3.zero;

            cubeSpawner.MoveChildren(zPos);
            finishSpawner.MoveChildren(zPos);
            cutSpawner.MoveChildren(zPos);
        }
    }
}