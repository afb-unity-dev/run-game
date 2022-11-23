using Com.Afb.RunGame.Presentation.Interactor;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Util;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class PlatformView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private CubeSpawner cubeSpawner;

        // Dependencies
        [Inject]
        private IPlatformInteractor platformInteractor;
        [Inject]
        private IPlatformPresenter platformPresenter;

        // Private Properties
        private CubeView currentCubeView;

        // Unity Methods
        private void Start() {
            platformPresenter.CharacterPosition.TakeUntilDestroy(gameObject).Subscribe(OnCharacterPosition);
        }

        // Public Methods
        public void OnClick() {
            float xPosition = currentCubeView.GetXPosition();
            platformInteractor.PlaceCube(xPosition);
        }

        // Private Methods
        private void OnCharacterPosition(int position) {
            float zPosition = position * Constants.CUBE_LENGTH + Constants.INITIAL_POSITION + Constants.CUBE_LENGTH / 2;
            currentCubeView = cubeSpawner.Spawn(zPosition);
        }

    }
}