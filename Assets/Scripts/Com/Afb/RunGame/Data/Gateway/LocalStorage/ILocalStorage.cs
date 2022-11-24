namespace Com.Afb.RunGame.Data.Gateway {
    public interface ILocalStorage {
        T Get<T>(string key, T defaultValue = default(T));
        void Set<T>(string key, T value);
    }
}
