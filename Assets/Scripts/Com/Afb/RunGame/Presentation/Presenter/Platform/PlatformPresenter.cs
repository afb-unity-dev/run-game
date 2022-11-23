using System;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class PlatformPresenter : IPlatformPresenter, IPlatformUpdatablePresenter {
        // Private Properties
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(0);
        private readonly Subject<bool> onResetPlatform = new Subject<bool>();

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;
        public IObservable<bool> OnResetPlatform => onResetPlatform;

        // Public Methods
        public void SetCharacterPosition(int position) {
            characterPosition.SetValueAndForceNotify(position);
        }

        public void SetTargetPosition(int position) {
            targetPosition.SetValueAndForceNotify(position);
        }

        public void SetResetPlatform(bool restart) {
            onResetPlatform.OnNext(restart);
        }
    }
}
