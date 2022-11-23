using Com.Afb.RunGame.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CubeSpawner : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private Transform cubeParent;

        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, CubeView> cubeViewPool;

        // Public Methods
        public CubeView Spawn(float zPosition) {
            var position = new Vector3(0, -Constants.CUBE_HEIGHT / 2, zPosition);
            return cubeViewPool.Spawn(cubeParent, position);
        }
    }
}