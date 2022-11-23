using Com.Afb.RunGame.Presentation.Interactor;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CubeView : MonoBehaviour, IPoolable<Transform, Vector3> {
        // Serialize Fields
        [SerializeField]
        private MovableXView movableXView;
        // Dependencies
        [Inject]
        private ICubeInteractor cubeInteractor;

        // Public Methods
        public void OnSpawned(Transform parent, Vector3 position) {
            transform.SetParent(parent);
            transform.localPosition = position;
            cubeInteractor.InitializeCube();
        }

        public void OnDespawned() {

        }

        public float GetXPosition() {
            return movableXView.transform.localPosition.x;
        }
    }
}
