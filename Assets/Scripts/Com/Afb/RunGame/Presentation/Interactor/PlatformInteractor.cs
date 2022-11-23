using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class PlatformInteractor : IPlatformInteractor {

        // Private Properties
        private IGameUseCase gameUseCase;
        private IPlatformPresenter platformPresenter;

        // Constructor
        public PlatformInteractor(IGameUseCase gameUseCase, IPlatformPresenter platformPresenter) {
            this.gameUseCase = gameUseCase;
            this.platformPresenter = platformPresenter;

            gameUseCase.CharacterPosition.Subscribe(OnCharacterPositionChange);
        }

        private void OnCharacterPositionChange(int position) {
            platformPresenter.SetCharacterPosition(position);
        }

        // Public Methods
        public void PlaceCube(float xPosition) {
            gameUseCase.PlaceCube(xPosition);
        }
    }
}
