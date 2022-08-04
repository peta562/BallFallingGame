namespace Core.SaveLoad {
    public interface ISaveLoadManager {
        public void Save(SaveData saveData);
        public SaveData Load();
    }
}