using System;
using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class PlatformInteractor : IPlatformInteractor, IPlatformCubeInteractor {

        // Readonly Properties
        private readonly ICubePlacementUseCase cubePlacementUseCase;
        private readonly IPlatformUseCase platformUseCase;
        private readonly IPlatformUpdatablePresenter platformUpdatablePresenter;
        private readonly ICubeCreateUseCase cubeCreateUseCase;

        // Constructor
        public PlatformInteractor(ICubePlacementUseCase cubePlacementUseCase,
                IPlatformUseCase platformUseCase,
                IPlatformUpdatablePresenter platformUpdatablePresenter,
                ICubeCreateUseCase cubeCreateUseCase) {

            this.platformUseCase = platformUseCase;
            this.cubePlacementUseCase = cubePlacementUseCase;
            this.platformUpdatablePresenter = platformUpdatablePresenter;
            this.cubeCreateUseCase = cubeCreateUseCase;

            platformUseCase.CharacterPosition.Subscribe(OnCharacterPositionChange);
            platformUseCase.TargetPosition.Subscribe(OnTargetPositionChange);
        }

        // Public Methods
        public void PlaceCube(float xPosition) {
            cubePlacementUseCase.PlaceCube(xPosition);
        }

        public void AddCube() {
            cubeCreateUseCase.CreateNewCube();
        }

        // Private Methods
        private void OnTargetPositionChange(int position) {
            platformUpdatablePresenter.SetTargetPosition(position);
        }

        private void OnCharacterPositionChange(int position) {
            platformUpdatablePresenter.SetCharacterPosition(position);
        }

    }
}
