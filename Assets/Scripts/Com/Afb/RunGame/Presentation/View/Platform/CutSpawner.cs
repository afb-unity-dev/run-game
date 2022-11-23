using Com.Afb.RunGame.Business.Model;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CutSpawner : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private Transform cutParent;

        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, CubeCutModel, CutView> cutViewPool;

        // Public Methods
        public CutView Spawn(CubeCutModel cut, Vector3 from) {
            var position = new Vector3(cut.XPosition, from.y, from.z);
            return cutViewPool.Spawn(cutParent, position, cut);
        }

        public void MoveChildren(float zPos) {
            foreach (Transform child in cutParent) {
                var localPos = child.localPosition;
                localPos.z += zPos;
                child.localPosition = localPos;
            }
        }
    }
}