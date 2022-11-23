using Com.Afb.RunGame.Business.Util;
using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public interface IGameStateUseCase {
        // Properties
        IReadOnlyReactiveProperty<GameSate> GameState { get; }

        // Methods
        void BeginPlaying();
        void ResetLevel();
    }
}
