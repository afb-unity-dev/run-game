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
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(10);
        private readonly Subject<CubeCutModel> lockCurrentCube = new Subject<CubeCutModel>();
        private readonly Subject<bool> gameOver = new Subject<bool>();

        // Private Properties
        private float lastPosition = 0;

        // Public Properties
        public IReadOnlyReactiveProperty<CurrentCubeModel> CurrentCube => currentCube;
        public IReadOnlyReactiveProperty<float> Speed => speed;
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;
        public IObservable<CubeCutModel> LockCurrentCube => lockCurrentCube;
        public IObservable<bool> GameOver => gameOver;

        // Constructor
        public GameUseCase() {
            Initialize();
        }

        // Private Methods
        private void Initialize() {
            var size = new Vector3(Constants.CUBE_WIDTH, Constants.CUBE_HEIGHT, Constants.CUBE_LENGTH);
            CreateCube(size);
        }

        private void CreateCube(Vector3 size) {
            var currentCube = new CurrentCubeModel();
            currentCube.Size = size;
            currentCube.Color = UnityEngine.Random.ColorHSV();
            currentCube.XPosition = UnityEngine.Random.Range(0, 2) == 0 ? Constants.LEFT_BOUNDARY : Constants.RIGHT_BOUNDARY;
            currentCube.IsMoving = true;
            this.currentCube.SetValueAndForceNotify(currentCube);
        }

        public void PlaceCube(float xPosition) {
            var cube = currentCube.Value;

            float prevPosition = lastPosition;
            float prevWidth = cube.Size.x;
            float currentWidth = cube.Size.x;

            if (characterPosition.Value == 0) {
                prevPosition = 0;
                prevWidth = Constants.INITIAL_WIDTH;
            }
            else {
                if (Mathf.Abs(prevPosition - xPosition) < Constants.POSITION_THRESHOLD) {
                    xPosition = prevPosition;
                }
            }

            float prevLeft = prevPosition - prevWidth / 2;
            float prevRight = prevPosition + prevWidth / 2;

            float currentLeft = xPosition - currentWidth / 2;
            float currentRight = xPosition + currentWidth / 2;

            Vector3 newSize = cube.Size;
            Vector3? cutSize = null;
            int cutDirection = 0;
            float? cutXPosition = null;

            // If left
            if (currentLeft < prevLeft) {
                float newWidth = Mathf.Max(currentRight - prevLeft, 0);
                float cutWidth = cube.Size.x - newWidth;
                newSize.x = newWidth;
                xPosition = prevLeft + newWidth / 2;
                cutSize = new Vector3(cube.Size.x - newWidth, cube.Size.y, cube.Size.z);
                cutXPosition = prevLeft - cutWidth / 2;
                cutDirection = 1;
            }
            // If right
            else if (currentRight > prevRight) {
                float newWidth = Mathf.Max(prevRight - currentLeft, 0);
                float cutWidth = cube.Size.x - newWidth;
                newSize.x = newWidth;
                xPosition = currentLeft + newWidth / 2;
                cutSize = new Vector3(cube.Size.x - newWidth, cube.Size.y, cube.Size.z);
                cutXPosition = prevRight + cutWidth / 2;
                cutDirection = -1;
            }

            lastPosition = xPosition;
            cube.Size = newSize;
            cube.XPosition = xPosition;
            cube.IsMoving = false;
            currentCube.SetValueAndForceNotify(cube);

            if (cutSize != null) {
                var cubeCutModel = new CubeCutModel();
                cubeCutModel.Size = cutSize.Value;
                cubeCutModel.XPosition = cutXPosition.Value;
                cubeCutModel.Color = cube.Color;
                cubeCutModel.Direction = cutDirection;
                lockCurrentCube.OnNext(cubeCutModel);
            }
            else {
                lockCurrentCube.OnNext(null);
            }

            if (newSize.x <= 0) {
                UnityEngine.Debug.Log("Game Over");
                gameOver.OnNext(false);
            }
            else {
                int newCharacterPos = characterPosition.Value + 1;


                if (newCharacterPos >= targetPosition.Value) {
                    UnityEngine.Debug.Log("Game Win");
                    gameOver.OnNext(true);
                }
                else {
                    CreateCube(newSize);
                    characterPosition.Value = newCharacterPos;
                }
            }
        }
    }
}
