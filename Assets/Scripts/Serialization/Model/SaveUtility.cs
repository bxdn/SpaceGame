using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public static class SaveUtility
    {
        public static void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, WorldGeneration.Galaxy);
            file.Close();
        }
        public static Galaxy LoadGame()
        {
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                Galaxy galaxy = (Galaxy)bf.Deserialize(file);
                file.Close();
                foreach (var colony in galaxy.Player.Colonies)
                    colony.FinishDeserialization();
                return galaxy;
            }
            return null;
        }
        public static void ClearSave()
        {
            if (SavedGameFound())
                File.Delete(Application.persistentDataPath + "/gamesave.save");
        }
        public static bool SavedGameFound()
        {
            return File.Exists(Application.persistentDataPath + "/gamesave.save");
        }
    }
}
