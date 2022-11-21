using Com.Afb.RunGame.Presentation.Interactor;
using Com.Afb.RunGame.Presentation.Presenter;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game.Cube {
    public class CubeInstaller : MonoInstaller {
        // Public Methods
        public override void InstallBindings() {
            BindPresenter();
            BindInteractor();
        }

        // Private Methods
        private void BindPresenter() {
            Container.BindInterfacesTo<CubePresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInteractor() {
            Container.BindInterfacesTo<CubeInteractor>()
                .AsSingle()
                .NonLazy();
        }
    }
}
