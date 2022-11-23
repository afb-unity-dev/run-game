using Com.Afb.RunGame.Presentation.Interactor;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class InteractorInstaller : Installer {
        // Public Methods
        public override void InstallBindings() {
            BindPlatformInteractor();
            BindGameInteractor();
            BindCharacterInteractor();
        }

        // Private Methods
        private void BindPlatformInteractor() {
            Container.BindInterfacesTo<PlatformInteractor>()
                .AsSingle()
                .Lazy();
        }

        private void BindGameInteractor() {
            Container.BindInterfacesTo<GameInteractor>()
                .AsSingle()
                .Lazy();
        }

        private void BindCharacterInteractor() {
            Container.BindInterfacesTo<CharacterInteractor>()
                .AsSingle()
                .Lazy();
        }
    }
}

