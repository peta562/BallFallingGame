using System.Threading.Tasks;
using Configs;

namespace Core.Loaders {
    public interface IConfigLoader {
        public Task<T> Load<T>(string id);
        public void Unload(BaseConfig config);
    }
}