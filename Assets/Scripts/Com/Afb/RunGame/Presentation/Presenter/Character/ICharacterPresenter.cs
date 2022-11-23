using UniRx;
using UnityEngine;

namespace Com.Afb.RunGame.Presentation.Presenter {
    public interface ICharacterPresenter {
        // Properties
        IReadOnlyReactiveProperty<Vector3> Position { get; }
        IReadOnlyReactiveProperty<bool> WillFall { get; }
    }
}
