using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class PlatformInteractor : IPlatformInteractor {

        // Readonly Properties
        private readonly ICubePlacementUseCase cubePlacementUseCase;
        private readonly IPlatformUseCase platformUseCase;
        private readonly IPlatformPresenter platformPresenter;

        // Constructor
        public PlatformInteractor(ICubePlacementUseCase cubePlacementUseCase,
                IPlatformUseCase platformUseCase,
                IPlatformPresenter platformPresenter) {

            this.platformUseCase = platformUseCase;
            this.cubePlacementUseCase = cubePlacementUseCase;
            this.platformPresenter = platformPresenter;

            platformUseCase.CharacterPosition.Subscribe(OnCharacterPositionChange);
        }

        private void OnCharacterPositionChange(int position) {
            platformPresenter.SetCharacterPosition(position);
        }

        // Public Methods
        public void PlaceCube(float xPosition) {
            cubePlacementUseCase.PlaceCube(xPosition);
        }
    }
}
