using Com.Afb.RunGame.Presentation.View.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class FinishView : MonoBehaviour, IPoolable<Transform, Vector3> {
        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, FinishView> finishViewPool;

        // Unity merhods
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == ViewConstants.DESPAWNER_TAG) {
                finishViewPool.Despawn(this);
            }
        }

        // Public Methods
        public void OnSpawned(Transform parent, Vector3 position) {
            transform.SetParent(parent);
            transform.localPosition = position;
        }

        public void OnDespawned() {

        }
    }
}
