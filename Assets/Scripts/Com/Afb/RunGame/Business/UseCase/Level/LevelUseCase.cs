using Com.Afb.RunGame.Data.Gateway;
using Com.Afb.RunGame.Util;
using UniRx;

namespace Com.Afb.RunGame.Business.UseCase {
    public class LevelUseCase : ILevelUseCase {

        // Readonly Properties
        private readonly ILocalStorage localStorage;
        private readonly ReactiveProperty<int> level = new ReactiveProperty<int>(0);

        // Public Properties
        public IReadOnlyReactiveProperty<int> Level => level;

        // Constructor
        public LevelUseCase(ILocalStorage localStorage) {
            this.localStorage = localStorage;
            level.Value = localStorage.Get<int>(Constants.LEVEL_PREFS);
        }

        // Public Methods
        public void IncrementLevel() {
            level.Value++;
            localStorage.Set(Constants.LEVEL_PREFS, level.Value);
        }
    }
}
