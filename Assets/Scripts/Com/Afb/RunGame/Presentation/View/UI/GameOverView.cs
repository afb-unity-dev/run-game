namespace Com.Afb.RunGame.Presentation.View {
    public class GameOverView : PopupView {
        // Public Methods
        public void OnClick() {
            gamePresenter.ResetLevel();
        }
    }

}