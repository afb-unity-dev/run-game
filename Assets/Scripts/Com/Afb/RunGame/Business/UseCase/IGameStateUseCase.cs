using System;

namespace Com.Afb.RunGame.Business.UseCase {
    public interface IGameStateUseCase {
        // Properties
        IObservable<bool> GameOver { get; }
    }
}
