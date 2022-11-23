using System;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class PlatformPresenter : IPlatformPresenter, IPlatformUpdatablePresenter {
        // Private Properties
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(0);
        private readonly Subject<bool> gameOver = new Subject<bool>();

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;
        public IObservable<bool> GameOver => gameOver;

        // Public Methods
        public void SetCharacterPosition(int position) {
            characterPosition.Value = position;
        }

        public void SetTargetPosition(int position) {
            targetPosition.Value = position;
        }

        public void SetGameOver(bool success) {
            gameOver.OnNext(success);
        }
    }
}
