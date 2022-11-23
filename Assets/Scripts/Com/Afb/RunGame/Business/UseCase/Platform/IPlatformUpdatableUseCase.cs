namespace Com.Afb.RunGame.Business.UseCase {
    public interface IPlatformUpdatableUseCase {
        // Methods
        void Continue();
        void ResetPlatform(bool restart);
    }
}
