using Com.Afb.RunGame.Presentation.Interactor;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CubeView : MonoBehaviour, IPoolable<Transform> {
        // Dependencies
        [Inject]
        private ICubeInteractor cubeInteractor;

        // TODO remove after test
        private void Start() {
            cubeInteractor.InitializeCube();
        }

        // Public Methods
        public void OnSpawned(Transform parent) {
            transform.SetParent(parent);
            cubeInteractor.InitializeCube();
        }

        public void OnDespawned() {

        }
    }
}
