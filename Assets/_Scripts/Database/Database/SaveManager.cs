using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager {

    static string SAVE_FILE_NAME = "ZSS.dat";

    public static void Save(User user)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + SAVE_FILE_NAME, FileMode.OpenOrCreate);
        bf.Serialize(file, user);
        file.Close();
    }

    public static bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SAVE_FILE_NAME))
        {
            Debug.Log("File does exist!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SAVE_FILE_NAME, FileMode.Open);
            UserManager.user = (User) bf.Deserialize(file);
            Debug.Log("Save file: " + UserManager.user.username + " " + UserManager.user.GetScore(StageEnum.STAGE1));
            file.Close();
            return true;
        }
        else
        {
            Debug.Log("File does not exist!");
            return false;
        }
    }

    //This one is null until UserManager.cs created the user. Currently unused for now.
    public void CreateSave()
    {
        if (!Load())
        {
            if(UserManager.user != null)
            {
                Debug.Log("Trying to create the new save file");
                Save(UserManager.user);
            }
            else
            {
                Debug.Log("User is null");
            }
        }
    }

}
