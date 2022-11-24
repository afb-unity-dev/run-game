using Com.Afb.RunGame.Presentation.Presenter;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class LevelValueView : MonoBehaviour {
        // Private Properties
        private TMP_Text levelText;

        // Dependencies
        [Inject]
        private ILevelPresenter levelPresenter;

        // Unity Methods
        private void Awake() {
            levelText = GetComponent<TMP_Text>();

            levelPresenter.Level
                .TakeUntilDestroy(gameObject)
                .Subscribe(OnLevelChange);
        }

        // Private Properties
        private void OnLevelChange(int level) {
            levelText.text = level.ToString();
        }
    }
}
