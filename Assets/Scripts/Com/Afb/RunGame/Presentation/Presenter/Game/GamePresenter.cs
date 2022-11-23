using Com.Afb.RunGame.Business.Util;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class GamePresenter : IGamePresenter, IGameUpdatablePresenter {
        // Private Properties
        private readonly ReactiveProperty<GameSate> gameState = new ReactiveProperty<GameSate>(GameSate.Ready);

        // Public Properties
        public IReadOnlyReactiveProperty<GameSate> GameState => gameState;

        // Public Methods
        public void SetGameState(GameSate gameSate) {
            gameState.SetValueAndForceNotify(gameSate);
        }
    }
}
