using System;
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
        private Transform movingPlatform;

        // Dependencies
        [Inject]
        private IPlatformInteractor platformInteractor;
        [Inject]
        private IPlatformPresenter platformPresenter;

        // Private Properties
        private CubeView currentCubeView;
        private int lastPosition = 0;

        // Unity Methods
        private void Start() {
            platformPresenter.CharacterPosition.TakeUntilDestroy(gameObject).Subscribe(OnCharacterPosition);
            platformPresenter.TargetPosition.TakeUntilDestroy(gameObject).Subscribe(OnTargetPosition);
            platformPresenter.GameOver.TakeUntilDestroy(gameObject).Subscribe(OnGameOver);
            finishSpawner.Spawn(0);
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
            float zPosition = position * Constants.CUBE_LENGTH + Constants.INITIAL_POSITION + Constants.FINISH_LEGHTH / 2;
            finishSpawner.Spawn(zPosition);
        }

        private void OnGameOver(bool success) {
            if (success) {
                float platformPos = (lastPosition + 1) * Constants.CUBE_LENGTH + Constants.FINISH_LEGHTH / 2;
                MovePlatform(platformPos);
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
    }
}