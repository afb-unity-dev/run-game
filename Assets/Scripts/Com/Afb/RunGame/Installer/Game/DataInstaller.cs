using Com.Afb.RunGame.Data.Gateway;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class DataInstaller : MonoInstaller {
        // Public Methods
        public override void InstallBindings() {
            BindLocalStorage();
        }

        // Private Methods
        private void BindLocalStorage() {
            Container.BindInterfacesTo<LocalStorage>()
                .AsSingle();
        }
    }
}