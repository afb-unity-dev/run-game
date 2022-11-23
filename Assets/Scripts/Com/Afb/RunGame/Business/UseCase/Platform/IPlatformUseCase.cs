using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public interface IPlatformUseCase {
        // Properties
        IReadOnlyReactiveProperty<int> CharacterPosition { get; }
        IReadOnlyReactiveProperty<int> TargetPosition { get; }
    }
}
