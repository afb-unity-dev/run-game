using System;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICubeScorePresenter {
        // Properties
        IObservable<int> PerfectScore { get; }
    }
}
