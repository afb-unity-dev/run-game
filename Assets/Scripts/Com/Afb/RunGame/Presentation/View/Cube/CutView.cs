using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Presentation.View.Util;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CutView : MonoBehaviour, IPoolable<Transform, Vector3, CubeCutModel> {
        // Dependencies
        [Inject]
        private MonoPoolableMemoryPool<Transform, Vector3, CubeCutModel, CutView> cutViewPool;

        // Private Properties
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        private Rigidbody body;

        // Unity Methods
        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
            body = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == ViewConstants.DESPAWNER_TAG) {
                cutViewPool.Despawn(this);
            }
        }

        // Public Methods
        public void OnSpawned(Transform parent, Vector3 position, CubeCutModel cutModel) {
            transform.SetParent(parent);
            SetPosition(position);
            SetMesh(cutModel.Size);
            SetColor(cutModel.Color);
            SetTorque(cutModel.Direction);
        }

        public void OnDespawned() {
            body.isKinematic = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.isKinematic = false;
        }

        // Private Methods
        private void SetPosition(Vector3 position) {
            transform.position = position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
            Vector3 torque = new Vector3(1f, 0.5f, 1 * direction);
            GetComponent<Rigidbody>().AddTorque(torque);
        }
    }
}
