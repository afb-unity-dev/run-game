using System;
using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Business.Util;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Util;
using UniRx;
using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class CharacterInteractor : IDisposable, ICharacterInteractor {
        // Readonly Properties
        private readonly CompositeDisposable disposables = new CompositeDisposable();
        private readonly IGameStateUseCase gameStateUseCase;
        private readonly ICubeUseCase cubeUseCase;
        private readonly IPlatformUseCase platformUseCase;
        private readonly ICharacterUpdatablePresenter characterUpdatablePresenter;

        // Private Properties
        private float xPosition = 0;
        private int lastPosition = 0;

        // Constructor
        public CharacterInteractor(IGameStateUseCase gameStateUseCase,
                ICubeUseCase cubeUseCase,
                IPlatformUseCase platformUseCase,
                ICharacterUpdatablePresenter characterUpdatablePresenter) {

            this.cubeUseCase = cubeUseCase;
            this.platformUseCase = platformUseCase;
            this.characterUpdatablePresenter = characterUpdatablePresenter;

            cubeUseCase.CurrentCube
                 .Subscribe(OnCurrentCubeChange)
                .AddTo(disposables);

            platformUseCase.CharacterPosition
                 .Subscribe(OnCharacterPositionChange)
                 .AddTo(disposables);

            gameStateUseCase.GameState
                .Subscribe(OnGameStateChange)
                .AddTo(disposables);
        }

        // Public Methods
        public void Dispose() {
            disposables.Dispose();
        }

        // Private Methods
        private void OnCurrentCubeChange(CurrentCubeModel currentCube) {
            if (currentCube != null) {
                xPosition = currentCube.XPosition;
            }
        }

        private void OnCharacterPositionChange(int position) {
            lastPosition = position;

            if (position == 0) {
                return;
            }

            float zPosition = (lastPosition - 1) * Constants.CUBE_LENGTH
                + Constants.INITIAL_POSITION
                + Constants.CUBE_LENGTH / 2;

            characterUpdatablePresenter.SetWillFall(false);
            MoveCharacter(zPosition);
        }

        private void OnGameStateChange(GameSate gameSate) {
            if (gameSate == GameSate.Complete) {
                xPosition = 0;
                float zPosition = (lastPosition + 1) * Constants.CUBE_LENGTH
                                   + Constants.INITIAL_POSITION
                                   + Constants.FINISH_LEGHTH / 2;

                characterUpdatablePresenter.SetWillFall(false);
                MoveCharacter(zPosition);
            }
            else if (gameSate == GameSate.Fail) {
                float zPosition = lastPosition * Constants.CUBE_LENGTH
                        + Constants.INITIAL_POSITION
                        + 0.25f;

                characterUpdatablePresenter.SetWillFall(true);
                MoveCharacter(zPosition);
            }
            else {
                characterUpdatablePresenter.SetWillFall(false);
            }
        }

        private void MoveCharacter(float zPosition) {
            characterUpdatablePresenter.SetPosition(new Vector3(xPosition, 0, zPosition));
        }
    }
}