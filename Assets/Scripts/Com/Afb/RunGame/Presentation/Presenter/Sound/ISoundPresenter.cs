using System;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ISoundPresenter {
        // Properties
        IObservable<string> Sound { get; }
    }
}
