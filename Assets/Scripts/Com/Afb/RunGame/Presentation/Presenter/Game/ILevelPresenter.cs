using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ILevelPresenter {
        IReadOnlyReactiveProperty<int> Level { get; }
    }
}
