using Com.Afb.RunGame.Business.Model;
using Com.Afb.RunGame.Presentation.Presenter;
using Com.Afb.RunGame.Presentation.View.Util;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class MeshView : MonoBehaviour {
        // Dependencies
        [Inject]
        private ICubeMeshPresenter cubeMeshPresenter;
        [Inject]
        private CutSpawner cutSpawner;

        // Private Properties
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        // Unity Methods
        private void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();

            cubeMeshPresenter.Size
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnSizeChange);

            cubeMeshPresenter.Color
              .TakeUntilDestroy(gameObject)
              .Subscribe(OnColorChange);

            cubeMeshPresenter.CubeCut
                 .TakeUntilDestroy(gameObject)
                 .Subscribe(OnCubeCut);

            cubeMeshPresenter.XPosition
                 .TakeUntilDestroy(gameObject)
                 .Subscribe(OnXPosition);
        }

        // Private Methods
        private void OnXPosition(float x) {
            var pos = transform.localPosition;
            pos.x = x;
            transform.localPosition = pos;
        }

        private void OnColorChange(Color color) {
            meshRenderer.material.color = color;
        }

        private void OnSizeChange(Vector3 size) {
            var mesh = meshFilter.mesh;

            if (size.x == 0) {
                mesh.Clear();
            }
            else {
                CubeCreator.GenerateCube(mesh, size);
                var collider = GetComponent<BoxCollider>();
                collider.size = size;
            }
        }

        private void OnCubeCut(CubeCutModel cut) {
            if (cut != null) {
                cutSpawner.Spawn(cut, transform.position);
            }

        }
    }
}
