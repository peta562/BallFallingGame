using System.IO;
using UnityEngine;

namespace Core.SaveLoad {
    public sealed class JsonSaveLoadManager : ISaveLoadManager {
        readonly string _filePath;

        public JsonSaveLoadManager() {
            _filePath = Application.persistentDataPath + "/Save.json";
        }

        public void Save(SaveData saveData) {
            var json = JsonUtility.ToJson(saveData);

            using (var writer = new StreamWriter(_filePath)) {
                writer.Write(json);
            }
        }

        public SaveData Load() {
            var json = "";

            if ( !File.Exists(_filePath) ) {
                return new SaveData();
            }

            using (var reader = new StreamReader(_filePath)) {
                string line;

                while ( (line = reader.ReadLine()) != null ) {
                    json += line;
                }
            }

            if ( string.IsNullOrEmpty(json) ) {
                return new SaveData();
            }

            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}