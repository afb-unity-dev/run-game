using Com.Afb.RunGame.Business.UseCase;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class BusinessInstaller : MonoInstaller {
        // Public Methods
        public override void InstallBindings() {
            BindGameUseCase();
        }

        // Private Methods
        private void BindGameUseCase() {
            Container.BindInterfacesTo<GameUseCase>()
                .AsSingle();
        }
    }
}