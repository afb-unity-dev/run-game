using System;
using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class PlatformInteractor : IDisposable, IPlatformInteractor, IPlatformCubeInteractor {

        // Readonly Properties
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly ICubePlacementUseCase cubePlacementUseCase;
        private readonly IPlatformUseCase platformUseCase;
        private readonly IPlatformUpdatablePresenter platformUpdatablePresenter;
        private readonly ICubeCreateUseCase cubeCreateUseCase;
        private readonly IGameStateUseCase gameStateUseCase;

        // Constructor
        public PlatformInteractor(ICubePlacementUseCase cubePlacementUseCase,
                IPlatformUseCase platformUseCase,
                IPlatformUpdatablePresenter platformUpdatablePresenter,
                ICubeCreateUseCase cubeCreateUseCase,
                IGameStateUseCase gameStateUseCase) {

            this.platformUseCase = platformUseCase;
            this.cubePlacementUseCase = cubePlacementUseCase;
            this.platformUpdatablePresenter = platformUpdatablePresenter;
            this.cubeCreateUseCase = cubeCreateUseCase;
            this.gameStateUseCase = gameStateUseCase;

            platformUseCase.CharacterPosition
                .Subscribe(OnCharacterPositionChange)
                .AddTo(disposables);
            platformUseCase.TargetPosition
                .Subscribe(OnTargetPositionChange)
                .AddTo(disposables);
            gameStateUseCase.GameOver
                .Subscribe(OnGameOver)
                .AddTo(disposables);
        }

        // Public Methods
        public void Dispose() {
            disposables.Dispose();
        }

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

        private void OnGameOver(bool success) {
            platformUpdatablePresenter.SetGameOver(success);
        }
    }
}
