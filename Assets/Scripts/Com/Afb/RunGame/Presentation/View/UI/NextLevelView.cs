namespace Com.Afb.RunGame.Presentation.View {
    public class NextLevelView : PopupView {
        // Public Methods
        public void OnClick() {
            gamePresenter.BeginPlaying();
        }
    }

}