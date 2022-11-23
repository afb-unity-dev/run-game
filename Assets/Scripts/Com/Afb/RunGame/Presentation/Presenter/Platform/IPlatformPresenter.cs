using System;
using UniRx;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface IPlatformPresenter {
        // Properties
        IReadOnlyReactiveProperty<int> CharacterPosition { get; }
        IReadOnlyReactiveProperty<int> TargetPosition { get; }
        IObservable<bool> GameOver { get; }
    }
}