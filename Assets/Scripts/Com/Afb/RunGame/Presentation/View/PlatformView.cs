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
        private Transform movingPlatformParent;

        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, CubeView> cubeViewPool;
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
            SpawnCube(zPosition);
        }

        private void SpawnCube(float zPosition) {
            var position = new Vector3(0, -Constants.CUBE_HEIGHT / 2, zPosition);
            currentCubeView = cubeViewPool.Spawn(movingPlatformParent, position);

        }
    }
}