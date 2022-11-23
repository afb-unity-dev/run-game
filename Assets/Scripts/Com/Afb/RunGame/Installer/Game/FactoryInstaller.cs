using Com.Afb.RunGame.Presentation.View;
using Com.Afb.RunGame.Presentation.View.Util.Factory;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Installers.Game {
    public class FactoryInstaller : MonoInstaller {
        // Serialize Fields
        [Header("Cube View")]
        [SerializeField]
        private GameObject cubePrefab;
        [SerializeField]
        private Transform defaultCubeParent;

        [Header("Cut View")]
        [SerializeField]
        private GameObject cutPrefab;
        [SerializeField]
        private Transform defaultCutParent;

        [Header("Finish View")]
        [SerializeField]
        private GameObject finishPrefab;
        [SerializeField]
        private Transform defaultFinishParent;

        // Public Methods
        public override void InstallBindings() {
            BindCubeFactory();
            BindCutFactory();
            BindFinishFactory();
        }

        // Private Methods
        private void BindCubeFactory() {
            Container.BindInterfacesTo<GenericPrefabFactory<CubeView>>()
                .FromNew()
                .AsSingle()
                .WithArguments(cubePrefab, defaultCubeParent)
                .Lazy();
        }

        private void BindCutFactory() {
            Container.BindInterfacesTo<GenericPrefabFactory<CutView>>()
                .FromNew()
                .AsSingle()
                .WithArguments(cutPrefab, defaultCutParent)
                .Lazy();
        }

        private void BindFinishFactory() {
            Container.BindInterfacesTo<GenericPrefabFactory<FinishView>>()
                .FromNew()
                .AsSingle()
                .WithArguments(finishPrefab, defaultFinishParent)
                .Lazy();
        }
    }
}
