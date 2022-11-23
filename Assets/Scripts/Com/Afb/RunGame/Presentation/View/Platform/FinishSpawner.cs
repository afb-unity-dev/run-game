using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class FinishSpawner : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private Transform finishParent;

        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, FinishView> finishViewPool;

        // Public Methods
        public FinishView Spawn(float zPosition) {
            return finishViewPool.Spawn(finishParent, new Vector3(0, 0, zPosition));
        }
    }
}