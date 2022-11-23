using Com.Afb.RunGame.Business.Util;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface IGamePresenter {
        // Properties
        IReadOnlyReactiveProperty<GameSate> GameState { get; }
    }
}