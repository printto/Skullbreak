using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public void Save(User user)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Zzzsave.dat", FileMode.OpenOrCreate);
        bf.Serialize(file, user);
        file.Close();
    }

    public bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Zzzsave.dat"))
        {
            Debug.Log("File does exist!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Zzzsave.dat", FileMode.Open);
            UserManager.user = (User) bf.Deserialize(file);
            Debug.Log("Save file: " + UserManager.user.username + " " + UserManager.user.userscore);
            file.Close();
            return true;
        }
        else
        {
            Debug.Log("File does not exist!");
            return false;
        }
    }

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
