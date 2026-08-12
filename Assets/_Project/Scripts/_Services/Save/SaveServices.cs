using UnityEngine;

namespace _Project.Scripts._Services.Save
{
    public class SaveServices
    {
        private const string SAVE_KEY = "PlayerSaveData";

        public bool HasSave()
        {
            return PlayerPrefs.HasKey(SAVE_KEY);
        }

        public void Save(PlayerSaveData playerSaveData)
        {
            string json = JsonUtility.ToJson(playerSaveData);

            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();
        }

        public PlayerSaveData Load()
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }
    }
}