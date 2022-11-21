using System;
using Com.Afb.RunGame.Business.Model;
using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public interface IGameUseCase {
        // Properties
        IReadOnlyReactiveProperty<float> Speed { get; }
        IReadOnlyReactiveProperty<CurrentCubeModel> CurrentCube { get; }
        IObservable<CubeCutModel> LockCurrentCube { get; }
    }
}
