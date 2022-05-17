using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.PackageManager;
using UnityEngine;

public class SaveManager
{
    public PlayerSave PlayerSave;

    private const string SaveFile = "/gamesave.save";
    private static readonly string SavePath = Application.persistentDataPath + SaveFile;

    private static SaveManager _instance;
    
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
        
        PlayerSave = PlayerSave.Instance;

        // load the game
        // if (File.Exists(SavePath))
        // {
        //     BinaryFormatter bf = new BinaryFormatter();
        //
        //     using (FileStream file = File.Open(SavePath, FileMode.Open))
        //     {
        //         try
        //         {
        //             PlayerSave = (PlayerSave)bf.Deserialize(file);
        //         }
        //         catch
        //         {
        //             PlayerSave = PlayerSave.Instance;
        //         }
        //
        //         file.Close();
        //     }
        // }
        // else // create the save
        // {
        //     PlayerSave = PlayerSave.Instance;
        // }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SavePath);
        bf.Serialize(file, PlayerSave);
        file.Close();
    }
        
        
}