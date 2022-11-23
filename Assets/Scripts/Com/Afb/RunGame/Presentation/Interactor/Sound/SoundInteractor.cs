using Com.Afb.RunGame.Presentation.Presenter;

namespace Com.Afb.RunGame.Presentation.Interactor {
    public class SoundInteractor : ISoundInteractor {

        // Readonly propertis
        private readonly ISoundUpdatablePresenter soundUpdatablePresenter;

        // Constructor
        public SoundInteractor(ISoundUpdatablePresenter soundUpdatablePresenter) {
            this.soundUpdatablePresenter = soundUpdatablePresenter;
        }


        public void PlaySound(string name) {
            soundUpdatablePresenter.PlaySound(name);
        }
    }
}