using Com.Afb.RunGame.Presentation.View.Util.InputHelper;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class ClickControllerView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private PlatformView platformView;

        // Dependencies
        [Inject]
        private IClickEvent clickEvent;


        // Unity Methods
        private void Awake() {
            clickEvent.Click
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnClick);
        }

        // Private Methods
        private void OnClick(InputData input) {
            platformView.OnClick();
        }
    }
}
