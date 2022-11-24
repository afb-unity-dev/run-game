using System;
using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Business.UseCase;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class CubeInteractor : ICubeInteractor {
        // Readonly Properties
        private readonly ICubeUseCase cubeUseCase;
        private readonly ICubeUpdatablePresenter cubeUpdatablePresenter;

        // Private Properties
        private bool isCurrent = false;

        public CubeInteractor(ICubeUseCase cubeUseCase, ICubeUpdatablePresenter cubeUpdatablePresenter) {
            this.cubeUseCase = cubeUseCase;
            this.cubeUpdatablePresenter = cubeUpdatablePresenter;
        }

        public void InitializeCube() {
            isCurrent = true;
            cubeUpdatablePresenter.Reset();

            cubeUseCase.Speed
                .TakeWhile(_ => isCurrent)
                .Subscribe(SetSpeed);

            cubeUseCase.CurrentCube
                .TakeWhile(_ => isCurrent)
                .Subscribe(OnCurrentCubeChange);

            cubeUseCase.LockCurrentCube
                .TakeWhile(_ => isCurrent)
                .Subscribe(OnLockCurrentCube);

            cubeUseCase.PerfectScore
                .TakeWhile(_ => isCurrent)
                .Subscribe(OnPerfectScore);
        }

        private void OnPerfectScore(int score) {
            cubeUpdatablePresenter.SetPerfectScore(score);
        }

        private void SetSpeed(float speed) {
            cubeUpdatablePresenter.SetSpeed(speed);
        }

        private void OnLockCurrentCube(CubeCutModel cubeCut) {
            isCurrent = false;
            cubeUpdatablePresenter.SetCut(cubeCut);
        }

        private void OnCurrentCubeChange(CurrentCubeModel currentCube) {
            cubeUpdatablePresenter.SetColor(currentCube.Color);
            cubeUpdatablePresenter.SetSize(currentCube.Size);
            cubeUpdatablePresenter.SetXPosition(currentCube.XPosition);
            cubeUpdatablePresenter.SetIsMoving(currentCube.IsMoving);
        }
    }
}
