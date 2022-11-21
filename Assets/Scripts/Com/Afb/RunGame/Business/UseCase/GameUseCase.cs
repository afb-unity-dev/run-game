using System;
using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Util;
using UniRx;
using UnityEngine;

namespace Com.Afb.RunGame.Business.UseCase {
    public class GameUseCase : IGameUseCase {
        // Readonly Properties
        private readonly ReactiveProperty<CurrentCubeModel> currentCube = new ReactiveProperty<CurrentCubeModel>(null);
        private readonly ReactiveProperty<float> speed = new ReactiveProperty<float>(5);
        private readonly Subject<CubeCutModel> lockCurrentCube = new Subject<CubeCutModel>();

        // Public Properties
        public IReadOnlyReactiveProperty<CurrentCubeModel> CurrentCube => currentCube;
        public IReadOnlyReactiveProperty<float> Speed => speed;
        public IObservable<CubeCutModel> LockCurrentCube => lockCurrentCube;

        // Constructor
        public GameUseCase() {
            Initialize();
        }

        // Private Methods
        private void Initialize() {
            var currentCube = new CurrentCubeModel();
            currentCube.Size = new Vector3(Constants.CUBE_WIDTH, Constants.CUBE_HEIGHT, Constants.CUBE_LENGTH);
            currentCube.Color = UnityEngine.Random.ColorHSV();
            this.currentCube.Value = currentCube;
        }
    }
}
