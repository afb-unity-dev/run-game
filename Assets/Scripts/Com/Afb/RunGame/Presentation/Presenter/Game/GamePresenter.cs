using Com.Afb.RunGame.Business.Util;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class GamePresenter : IGamePresenter, IGameUpdatablePresenter, ILevelPresenter {
        // Private Properties
        private readonly ReactiveProperty<GameSate> gameState = new ReactiveProperty<GameSate>(GameSate.Ready);
        private readonly ReactiveProperty<int> level = new ReactiveProperty<int>(0);

        // Public Properties
        public IReadOnlyReactiveProperty<GameSate> GameState => gameState;
        public IReadOnlyReactiveProperty<int> Level => level;

        // Public Methods
        public void SetGameState(GameSate gameSate) {
            gameState.SetValueAndForceNotify(gameSate);
        }

        public void SetLevel(int level) {
            this.level.Value = level;
        }
    }
}
