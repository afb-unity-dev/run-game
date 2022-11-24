using Com.Afb.RunGame.Business.Util;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface IGameUpdatablePresenter {
        // Methods
        void SetGameState(GameSate gameSate);
        void SetLevel(int level);
    }
}