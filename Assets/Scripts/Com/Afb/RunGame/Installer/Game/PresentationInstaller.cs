using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class PresentationInstaller : MonoInstaller {
        // Public Methods
        public override void InstallBindings() {
            BindPresenter();
            BindInteractor();
            BindView();
        }

        // Private Methods
        private void BindPresenter() {
            Container.Install<PresentaterInstaller>();
        }

        private void BindInteractor() {
            Container.Install<InteractorInstaller>();
        }

        private void BindView() {
            Container.Install<ViewInstaller>();
        }
    }
}
