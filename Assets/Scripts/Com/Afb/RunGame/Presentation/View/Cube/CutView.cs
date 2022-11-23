using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Presentation.View.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CutView : MonoBehaviour, IPoolable<Vector3, CubeCutModel> {
        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Vector3, CubeCutModel, CutView> cutViewPool;

        // Private Properties
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        // Unity Methods
        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == ViewConstants.DESPAWNER_TAG) {
                cutViewPool.Despawn(this);
            }
        }

        // Public Methods
        public void OnSpawned(Vector3 position, CubeCutModel cutModel) {
            SetPosition(position);
            SetMesh(cutModel.Size);
            SetColor(cutModel.Color);
            SetTorque(cutModel.Direction);
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

        private void SetTorque(int direction) {
            Vector3 torque = new Vector3(0.5f, 0, 1 * direction);
            GetComponent<Rigidbody>().AddTorque(torque);
        }


    }
}
