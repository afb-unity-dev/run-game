using System;
using Com.Afb.RunGame.Business.Model;
using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public interface IGameUseCase {
        // Properties
        IReadOnlyReactiveProperty<float> Speed { get; }
        IReadOnlyReactiveProperty<CurrentCubeModel> CurrentCube { get; }
        IReadOnlyReactiveProperty<int> CharacterPosition { get; }
        IReadOnlyReactiveProperty<int> TargetPosition { get; }
        IObservable<CubeCutModel> LockCurrentCube { get; }
        IObservable<bool> GameOver { get; }

        // Methods
        void PlaceCube(float xPosition);
    }
}
