using Com.Afb.RunGame.Presentation.Interactor;
using Com.Afb.RunGame.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CubeSpawner : GenericCubeSpawner<CubeView> {
        // Dependencies
        [Inject]
        private IPlatformCubeInteractor platformCubeInteractor;

        // Public Methods
        public override CubeView Spawn(float zPosition) {
            platformCubeInteractor.AddCube();
            var position = new Vector3(0, -Constants.CUBE_HEIGHT / 2, zPosition);
            return viewPool.Spawn(parent, position);
        }
    }
}