namespace Com.Afb.RunGame.Presentation.View {
    public class PlayLevelView : PopupView {
        // Public Methods
        public void OnClick() {
            gamePresenter.BeginPlaying();
        }
    }

}