using System;
using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public class GameStateUseCase : IGameStateUseCase, IGameStateUpdatableUseCase {
        // Readonly Properties
        private readonly Subject<bool> gameOver = new Subject<bool>();

        // Public Properties
        public IObservable<bool> GameOver => gameOver;

        // Public Methods
        public void SetGameOver(bool success) {
            gameOver.OnNext(success);
            UnityEngine.Debug.Log("GameOver: " + success);
        }
    }
}
