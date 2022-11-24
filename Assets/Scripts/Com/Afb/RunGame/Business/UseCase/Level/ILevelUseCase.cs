using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public interface ILevelUseCase {
        // Properties
        IReadOnlyReactiveProperty<int> Level { get; }

        // Methods
        void IncrementLevel();
    }
}
