using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Presentation.View;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class PoolInstaller : MonoInstaller {
        // Public Methods
        public override void InstallBindings() {
            BindCubePool();
            BindCutPool();
            BindFinishPool();
        }

        // Private Methods
        private void BindCubePool() {
            Container.Bind<MonoPoolableMemoryPool<Transform, Vector3, CubeView>>()
                .FromNew()
                .AsSingle()
                .WithArguments(
                    new MemoryPoolSettings(
                        // Initial size
                        16,
                        // Max size
                        int.MaxValue,
                        PoolExpandMethods.OneAtATime)
                );
        }

        private void BindCutPool() {
            Container.Bind<MonoPoolableMemoryPool<Transform, Vector3, CubeCutModel, CutView>>()
                .FromNew()
                .AsSingle()
                .WithArguments(
                    new MemoryPoolSettings(
                        // Initial size
                        2,
                        // Max size
                        int.MaxValue,
                        PoolExpandMethods.OneAtATime)
                );
        }

        private void BindFinishPool() {
            Container.Bind<MonoPoolableMemoryPool<Transform, Vector3, FinishView>>()
                .FromNew()
                .AsSingle()
                .WithArguments(
                    new MemoryPoolSettings(
                        // Initial size
                        2,
                        // Max size
                        int.MaxValue,
                        PoolExpandMethods.OneAtATime)
                );
        }
    }
}
