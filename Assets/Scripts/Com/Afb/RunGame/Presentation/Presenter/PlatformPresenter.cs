using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class PlatformPresenter : IPlatformPresenter {
        // Private Properties
        private readonly ReactiveProperty<int> characterPosition = new ReactiveProperty<int>(0);

        // Public Properties
        public IReadOnlyReactiveProperty<int> CharacterPosition => characterPosition;

        // Public Methods
        public void SetCharacterPosition(int position) {
            characterPosition.Value = position;
        }
    }
}
