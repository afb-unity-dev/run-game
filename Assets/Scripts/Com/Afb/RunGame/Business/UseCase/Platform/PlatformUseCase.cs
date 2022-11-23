using System;
using UniRx;
using Zenject;

namespace Com.Afb.RunGame.Business.UseCase {
    public class PlatformUseCase : IPlatformUseCase, IPlatformUpdatableUseCase {
        // Readonly Properties
        private readonly LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUseCase;
        private readonly LazyInject<WeakReference<ICubeUpdatableUseCase>> cubeUseCase;
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(4);

        // Private Properties
        private float lastPosition = 0;

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;

        // Constructor
        public PlatformUseCase(LazyInject<WeakReference<ICubeUpdatableUseCase>> cubeUseCase,
                LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUseCase) {

            this.cubeUseCase = cubeUseCase;
            this.gameStateUseCase = gameStateUseCase;
        }

        // Public Methods
        public void Continue() {
            int newCharacterPos = characterPosition.Value + 1;

            if (newCharacterPos >= targetPosition.Value) {
                if (gameStateUseCase.Value.TryGetTarget(out var target)) {
                    target.SetGameOver(true);
                }
            }
            else {
                characterPosition.Value = newCharacterPos;
            }
        }
    }
}
