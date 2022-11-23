using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICubeMovePresenter {
        // Properties
        IReadOnlyReactiveProperty<bool> IsMoving { get; }
        IReadOnlyReactiveProperty<float> MoveSpeed { get; }
    }
}
