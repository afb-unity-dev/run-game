using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface IPlatformPresenter {
        // Properties
        IReadOnlyReactiveProperty<int> CharacterPosition { get; }

        // Methods
        void SetCharacterPosition(int position);
    }
}