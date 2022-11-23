using Com.Afb.RunGame.Presentation.Presenter;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class CameraFollowView : MonoBehaviour {
        // Dependencies
        [Inject]
        private ICharacterPresenter characterPresenter;
        [Inject]
        private IPlatformPresenter platformPresenter;

        // Unity methods
        private void Start() {
            characterPresenter.Position
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnCharacterPosition);

            platformPresenter.OnResetPlatform
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnResetPlatform);
        }

        // Private Methods
        private void OnCharacterPosition(Vector3 position) {
            transform.DOMoveX(position.x, 0.5f);
        }

        private void OnResetPlatform(bool restart) {
            Reset();
        }

        private void Reset() {
            OnCharacterPosition(Vector3.zero);
        }
    }
}
