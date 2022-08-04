using Core.SaveLoad;

namespace Core {
    public abstract class BaseController {
        public abstract void Init();
        public abstract void DeInit();

        public virtual void Update() { }
        public virtual void Save(SaveData saveData) { }
        public virtual void Load(SaveData saveData) { }
    }
}