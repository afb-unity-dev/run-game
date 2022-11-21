using Com.Afb.RunGame.Presentation.View.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CutView : MonoBehaviour, IPoolable<Vector3, Vector3, Color> {
        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Vector3, Vector3, Color, CutView> cutViewPool;

        // Private Properties
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        // Unity Methods
        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
        }

        // Public Methods
        public void OnSpawned(Vector3 position, Vector3 size, Color color) {
            SetPosition(position);
            SetMesh(size);
            SetColor(color);
        }

        public void OnDespawned() {
        }

        // Private Methods
        private void SetPosition(Vector3 position) {
            transform.position = position;
        }

        private void SetColor(Color color) {
            meshRenderer.material.color = color;
        }

        private void SetMesh(Vector3 size) {
            var mesh = meshFilter.mesh;
            CubeCreator.GenerateCube(mesh, size);
            var collider = GetComponent<BoxCollider>();
            collider.size = size;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Despawner") {
                cutViewPool.Despawn(this);
            } 
        }
    }
}
