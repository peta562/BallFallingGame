namespace Core {
    public abstract class BaseController {
        public abstract void Init();

        public virtual void Update() { }

        public abstract void DeInit();
    }
}