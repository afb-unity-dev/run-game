using System;
using Com.Afb.RunGame.Business.UseCase;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class BusinessInstaller : MonoInstaller {
        // Public Methods
        public override void InstallBindings() {
            BindGameStateUseCase();
            BindPlatformUseCase();
            BindCubeUseCase();
            BindLevelUseCase();
        }

        // Private Methods
        private void BindGameStateUseCase() {
            Container.BindInterfacesTo<GameStateUseCase>()
                .AsSingle();

            Container.Bind<WeakReference<IGameStateUpdatableUseCase>>()
                .FromMethod(() =>
                    new WeakReference<IGameStateUpdatableUseCase>(
                        Container.Resolve<IGameStateUpdatableUseCase>()));
        }

        private void BindCubeUseCase() {
            Container.BindInterfacesTo<CubeUseCase>()
                .AsSingle();

            Container.Bind<WeakReference<ICubeUpdatableUseCase>>()
                .FromMethod(() =>
                    new WeakReference<ICubeUpdatableUseCase>(
                        Container.Resolve<ICubeUpdatableUseCase>()));
        }

        private void BindPlatformUseCase() {
            Container.BindInterfacesTo<PlatformUseCase>()
                .AsSingle();

            Container.Bind<WeakReference<IPlatformUpdatableUseCase>>()
                .FromMethod(() =>
                    new WeakReference<IPlatformUpdatableUseCase>(
                        Container.Resolve<IPlatformUpdatableUseCase>()));
        }

        private void BindLevelUseCase() {
            Container.BindInterfacesTo<LevelUseCase>()
                .AsSingle();
        }
    }
}