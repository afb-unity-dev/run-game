namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface IPlatformUpdatablePresenter {
        // Methods
        void SetCharacterPosition(int position);
        void SetTargetPosition(int position);
        void SetResetPlatform(bool restart);
    }
}