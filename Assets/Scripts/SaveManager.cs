using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    private static SaveManager _instance;
        
    public PlayerSave PlayerSave { get; set; }
    private const string SaveFile = "/gamesave.save";
    private static readonly string SavePath = Application.persistentDataPath + SaveFile;
    
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }

    private SaveManager()
    {
        // load the game
        if (File.Exists(SavePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SavePath, FileMode.Open);
            PlayerSave = (PlayerSave)bf.Deserialize(file);
            file.Close();
        }
        else // create the save
        {
            PlayerSave = new PlayerSave();
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SavePath);
        bf.Serialize(file, PlayerSave);
        file.Close();
    }
        
        
}