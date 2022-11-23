using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class GameInteractor : IGameInteractor {
        // Readonly Properties
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly IGameUpdatablePresenter gameUpdatablePresenter;
        private readonly IGameStateUseCase gameStateUseCase;

        // Constructor
        public GameInteractor(IGameUpdatablePresenter gameUpdatablePresenter,
                IGameStateUseCase gameStateUseCase) {

            this.gameUpdatablePresenter = gameUpdatablePresenter;
            this.gameStateUseCase = gameStateUseCase;

            gameStateUseCase.GameState
                .Subscribe(OnGameStateChange)
                .AddTo(disposables);
        }

        // Public Methods
        public void Dispose() {
            disposables.Dispose();
        }

        public void BeginPlaying() {
            gameStateUseCase.BeginPlaying();
        }

        public void ResetLevel() {
            gameStateUseCase.ResetLevel();
        }

        // Private Methods
        private void OnGameStateChange(GameSate gameState) {
            gameUpdatablePresenter.SetGameState(gameState);
        }
    }
}