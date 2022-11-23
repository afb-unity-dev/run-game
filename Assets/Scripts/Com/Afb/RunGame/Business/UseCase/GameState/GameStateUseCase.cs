using System;
using Com.Afb.RunGame.Business.Util;
using UniRx;
using Zenject;

namespace Com.Afb.RunGame.Business.UseCase {
    public class GameStateUseCase : IGameStateUseCase, IGameStateUpdatableUseCase {
        // Readonly Properties
        private readonly LazyInject<WeakReference<IPlatformUpdatableUseCase>> platformUseCase;
        private readonly ReactiveProperty<GameSate> gameState = new ReactiveProperty<GameSate>(GameSate.Ready);

        // Public Properties
        public IReadOnlyReactiveProperty<GameSate> GameState => gameState;

        // Constructor

        public GameStateUseCase(LazyInject<WeakReference<IPlatformUpdatableUseCase>> platformUseCase) {
            this.platformUseCase = platformUseCase;
        }

        // Public Methods
        public void SetGameOver(bool success) {
            if (success) {
                gameState.SetValueAndForceNotify(GameSate.Complete);
            }
            else {
                gameState.SetValueAndForceNotify(GameSate.Fail);
            }
        }

        public void BeginPlaying() {
            var prevState = gameState.Value;
            gameState.SetValueAndForceNotify(GameSate.Playing);

            if (prevState == GameSate.Complete) {
                if (platformUseCase.Value.TryGetTarget(out var target)) {
                    target.ResetPlatform(false);
                }
            }
        }

        public void ResetLevel() {
            var prevState = gameState.Value;
            gameState.SetValueAndForceNotify(GameSate.Playing);

            if (prevState == GameSate.Fail) {
                if (platformUseCase.Value.TryGetTarget(out var target)) {
                    target.ResetPlatform(true);
                }
            }
        }
    }
}
