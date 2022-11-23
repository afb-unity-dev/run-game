using System;
using UniRx;
using Zenject;

namespace Com.Afb.RunGame.Business.UseCase {
    public class PlatformUseCase : IPlatformUseCase, IPlatformUpdatableUseCase {
        // Readonly Properties
        private readonly LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUpdatableUseCase;
        private readonly LazyInject<WeakReference<ICubeUpdatableUseCase>> cubeUseCase;
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(4);
        private readonly Subject<bool> onResetPlatform = new Subject<bool>();

        private int lastCharacterPosition;

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;
        public IObservable<bool> OnResetPlatform => onResetPlatform;

        // Constructor
        public PlatformUseCase(LazyInject<WeakReference<ICubeUpdatableUseCase>> cubeUseCase,
                LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUpdatableUseCase) {

            this.cubeUseCase = cubeUseCase;
            this.gameStateUpdatableUseCase = gameStateUpdatableUseCase;
        }

        // Public Methods
        public void Continue() {
            lastCharacterPosition = characterPosition.Value + 1;

            if (lastCharacterPosition >= targetPosition.Value) {
                if (gameStateUpdatableUseCase.Value.TryGetTarget(out var target)) {
                    target.SetGameOver(true);
                }
            }
            else {
                characterPosition.Value = lastCharacterPosition;
            }
        }

        public void ResetPlatform(bool restart) {
            onResetPlatform.OnNext(restart);
            characterPosition.SetValueAndForceNotify(0);
            targetPosition.SetValueAndForceNotify(4);
            if (cubeUseCase.Value.TryGetTarget(out var cube)) {
                cube.Reset();
            }
        }
    }
}
