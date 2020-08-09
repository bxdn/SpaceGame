using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public static class Save
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
                return galaxy;
            }
            return null;
        }
        public static bool SavedGameFound()
        {
            return File.Exists(Application.persistentDataPath + "/gamesave.save");
        }
    }
}
