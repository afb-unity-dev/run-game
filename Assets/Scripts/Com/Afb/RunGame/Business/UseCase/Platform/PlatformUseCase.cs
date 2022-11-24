using System;
using Com.Afb.RunGame.Business.Util;
using UniRx;
using Zenject;

namespace Com.Afb.RunGame.Business.UseCase {
    public class PlatformUseCase : IDisposable, IPlatformUseCase, IPlatformUpdatableUseCase {
        // Readonly Properties
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUpdatableUseCase;
        private readonly LazyInject<WeakReference<ICubeUpdatableUseCase>> cubeUseCase;
        private readonly ILevelUseCase levelUseCase;
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(0);
        private readonly Subject<bool> onResetPlatform = new Subject<bool>();

        // Private Properties
        private int lastCharacterPosition;
        private int level = -1;

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;
        public IObservable<bool> OnResetPlatform => onResetPlatform;

        // Constructor
        public PlatformUseCase(LazyInject<WeakReference<ICubeUpdatableUseCase>> cubeUseCase,
                LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUpdatableUseCase,
                ILevelUseCase levelUseCase) {

            this.cubeUseCase = cubeUseCase;
            this.gameStateUpdatableUseCase = gameStateUpdatableUseCase;
            this.levelUseCase = levelUseCase;

            levelUseCase.Level.Subscribe(OnLevelChange).AddTo(disposables);
        }

        // Public Methods
        public void Dispose() {
            disposables.Dispose();
        }

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
            int target = PlatformTarget.Get(level);
            targetPosition.SetValueAndForceNotify(target);
            if (cubeUseCase.Value.TryGetTarget(out var cube)) {
                cube.Reset();
            }
        }

        // Private Properties
        private void OnLevelChange(int level) {
            if (this.level < 0) {
                int target = PlatformTarget.Get(level);
                targetPosition.SetValueAndForceNotify(target);
            }

            this.level = level;
        }
    }
}
