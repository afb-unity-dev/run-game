using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Util;
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
        private IGameUseCase gameUseCase; // TODO delete

        // Private Properties
        private CubeView currentCubeView;

        private void Start() {
            var position =  new Vector3(0, -Constants.CUBE_HEIGHT / 2, 1.5f);
            currentCubeView = cubeViewPool.Spawn(movingPlatformParent, position);
        }

        public void OnClick() {
            float xPosition = currentCubeView.GetXPosition();
            gameUseCase.PlaceCube(xPosition);
        }
    }
}