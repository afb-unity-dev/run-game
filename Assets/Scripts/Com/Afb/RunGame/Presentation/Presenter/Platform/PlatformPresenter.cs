using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class PlatformPresenter : IPlatformPresenter, IPlatformUpdatablePresenter {
        // Private Properties
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> targetPosition = new ReactiveProperty<int>(0);

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;
        public IReadOnlyReactiveProperty<int> TargetPosition => targetPosition;

        // Public Methods
        public void SetCharacterPosition(int position) {
            characterPosition.Value = position;
        }

        public void SetTargetPosition(int position) {
            targetPosition.Value = position;
        }
    }
}
