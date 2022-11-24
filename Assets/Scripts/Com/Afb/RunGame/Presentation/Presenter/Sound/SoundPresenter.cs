using System;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public class SoundPresenter : ISoundPresenter, ISoundUpdatablePresenter {
        // Readonly Properties
        private readonly Subject<string> sound = new Subject<string>();

        // Public Properties
        public IObservable<string> Sound => sound;

        // Public Methods
        public void PlaySound(string name) {
            sound.OnNext(name);
        }
    }
}