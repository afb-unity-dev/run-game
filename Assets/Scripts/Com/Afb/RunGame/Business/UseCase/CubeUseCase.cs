using System;
using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Util;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Business.UseCase {
    public class CubeUseCase : ICubeUseCase, ICubeUpdatableUseCase, ICubePlacementUseCase {
        // Readonly Properties
        private readonly LazyInject<WeakReference<IPlatformUpdatableUseCase>> platformUseCase;
        private readonly LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUseCase;
        private readonly ReactiveProperty<CurrentCubeModel> currentCube = new ReactiveProperty<CurrentCubeModel>(null);
        private readonly ReactiveProperty<float> speed = new ReactiveProperty<float>(5);
        private readonly Subject<CubeCutModel> lockCurrentCube = new Subject<CubeCutModel>();

        // Private Properties
        private float lastPosition;
        private float lastWidth;
        private Vector3 lastSize;

        // Public Properties
        public IReadOnlyReactiveProperty<CurrentCubeModel> CurrentCube => currentCube;
        public IReadOnlyReactiveProperty<float> Speed => speed;
        public IObservable<CubeCutModel> LockCurrentCube => lockCurrentCube;

        // Constructor
        public CubeUseCase(LazyInject<WeakReference<IPlatformUpdatableUseCase>> platformUseCase,
                LazyInject<WeakReference<IGameStateUpdatableUseCase>> gameStateUseCase) {

            this.platformUseCase = platformUseCase;
            this.gameStateUseCase = gameStateUseCase;

            Reset();
        }

        // Public Methods
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
                lockCurrentCube.OnNext(cubeCutModel);
            }
            else {
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
    }
}
