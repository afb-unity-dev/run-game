using System;
using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Util;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Business.UseCase {
    public class CubeUseCase : IDisposable, ICubeUseCase, ICubeUpdatableUseCase, ICubePlacementUseCase, ICubeCreateUseCase {
        // Readonly Properties
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly LazyInject<WeakReference<IPlatformUpdatableUseCase>> platformUseCase;
        private readonly LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUseCase;
        private readonly ILevelUseCase levelUseCase;
        private readonly ReactiveProperty<CurrentCubeModel> currentCube = new ReactiveProperty<CurrentCubeModel>(null);
        private readonly ReactiveProperty<float> speed = new ReactiveProperty<float>(5);
        private readonly Subject<CubeCutModel> lockCurrentCube = new Subject<CubeCutModel>();
        private readonly Subject<int> perfectScore = new Subject<int>();

        // Private Properties
        private float lastPosition;
        private float lastWidth;
        private Vector3 lastSize;
        private int lastScore = -1;

        // Public Properties
        public IReadOnlyReactiveProperty<CurrentCubeModel> CurrentCube => currentCube;
        public IReadOnlyReactiveProperty<float> Speed => speed;
        public IObservable<CubeCutModel> LockCurrentCube => lockCurrentCube;
        public IObservable<int> PerfectScore => perfectScore;

        // Constructor
        public CubeUseCase(LazyInject<WeakReference<IPlatformUpdatableUseCase>> platformUseCase,
                LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUseCase,
                ILevelUseCase levelUseCase) {

            this.platformUseCase = platformUseCase;
            this.gameStateUseCase = gameStateUseCase;
            this.levelUseCase = levelUseCase;

            levelUseCase.Level.Subscribe(OnLevelChange).AddTo(disposables);

            Reset();
        }

        // Public Methods
        public void Dispose() {
            disposables.Dispose();
        }

        public void Reset() {
            lastSize = new Vector3(Constants.CUBE_WIDTH, Constants.CUBE_HEIGHT, Constants.CUBE_LENGTH);
            lastPosition = 0;
            lastWidth = Constants.INITIAL_WIDTH;
        }

        public void CreateNewCube() {
            var currentCubeModel = CreateCube.Create(lastSize);
            currentCube.SetValueAndForceNotify(currentCubeModel);
        }

        public void PlaceCube(float currentPosition) {
            var cube = currentCube.Value;
            cube.IsMoving = false;
            cube.XPosition = currentPosition;

            var cubeCutModel = CheckCubePlacement.Check(cube, lastPosition, lastWidth);
            currentCube.SetValueAndForceNotify(cube);

            lastSize = cube.Size;
            lastPosition = cube.XPosition;
            lastWidth = cube.Size.x;

            if (cubeCutModel != null) {
                if (cube.Size.x <= 0) {
                    lastScore = -1;
                }
                else {
                    lastScore = 0;
                }

                perfectScore.OnNext(lastScore);

                lockCurrentCube.OnNext(cubeCutModel);
            }
            else {
                if (lastScore < -1) {
                    lastScore = -1;
                }

                lastScore++;
                perfectScore.OnNext(lastScore);

                lockCurrentCube.OnNext(null);
            }

            if (cube.Size.x <= 0) {
                if (gameStateUseCase.Value.TryGetTarget(out var target)) {
                    target.SetGameOver(false);
                }
            }
            else {
                if (platformUseCase.Value.TryGetTarget(out var target)) {
                    target.Continue();
                }
            }
        }

        // Private Methods
        private void OnLevelChange(int level) {
            float speed = CubeSpeed.Get(level);
            this.speed.SetValueAndForceNotify(speed);
        }

    }
}
