using Com.Afb.RunGame.Presentation.View.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class DespawnableCube : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private CubeView cubeView;

        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, CubeView> cubeViewPool;

        // Unity Functions
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == ViewConstants.DESPAWNER_TAG) {
                cubeViewPool.Despawn(cubeView);
            }
        }
    }
}
