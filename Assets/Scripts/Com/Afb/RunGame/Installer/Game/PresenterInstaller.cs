using Com.Afb.RunGame.Presentation.Presenter;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class PresentaterInstaller : Installer {
        // Public Methods
        public override void InstallBindings() {
            BindPlatformPresenter();
            BindGamePresenter();
            BindCharacterPresenter();
        }

        // Private Methods
        private void BindPlatformPresenter() {
            Container.BindInterfacesTo<PlatformPresenter>()
                .AsSingle()
                .Lazy();
        }

        private void BindGamePresenter() {
            Container.BindInterfacesTo<GamePresenter>()
                .AsSingle()
                .Lazy();
        }

        private void BindCharacterPresenter() {
            Container.BindInterfacesTo<CharacterPresenter>()
                .AsSingle()
                .Lazy();
        }
    }
}
