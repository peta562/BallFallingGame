namespace Core.Score {
    public interface IScoreProvider {
        public void Init();
        public void DeInit();
        public int GetScore();
    }
}