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
        }

        // Private Methods
        private void OnColorChange(Color obj) {
            meshRenderer.material.color = obj;
        }

        private void OnSizeChange(Vector3 size) {
            var mesh = meshFilter.mesh;
            CubeCreator.GenerateCube(mesh, size);
            var collider = GetComponent<BoxCollider>();
            collider.size = size;
        }

        private void OnCubeCut(CubeCutModel cut) {
        }
    }
}
