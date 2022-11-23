using System;
using System.Collections.Generic;
using Com.Afb.RunGame.Presentation.Presenter;
using UniRx;
using UnityEngine;
using Zenject;

namespace Com.Afb.RunGame.Presentation.View {
    public class SoundView : MonoBehaviour {
        // Serialize Fields
        [SerializeField]
        private List<AudioClip> notes;

        // Dependencies
        [Inject]
        private ISoundPresenter soundPresenter;

        // Readonly Properties
        private readonly Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

        // Private Properties
        private AudioSource audioSource;

        // Unity Methods
        private void Awake() {
            foreach (var clip in notes) {
                sounds[clip.name] = clip;
            }

            audioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            soundPresenter.Sound
                .TakeUntilDestroy(gameObject)
                .Subscribe(PlaySound);
        }

        private void PlaySound(string name) {
            if (sounds.TryGetValue(name, out var clip)) {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
