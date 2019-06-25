using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserSaveManager : MonoBehaviour {

    public void Save(User user)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Zzzsave.dat", FileMode.OpenOrCreate);
        bf.Serialize(file, user);
        file.Close();
    }

    public User Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Zzzsave.dat"))
        {
            Debug.Log("File does exist!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Zzzsave.dat", FileMode.Open);
            User user = (User) bf.Deserialize(file);
            Debug.Log("Save file: " + user.username + " " + user.userscore);
            file.Close();
            return user;
        }
        else
        {
            Debug.Log("File does not exist!");
            return null;
        }
    }

    public void CreateSave()
    {
        if (Load() == null)
        {
            Debug.Log("Trying to create the new save file");
            Save(new User("Me test", 0));
        }
    }

}
