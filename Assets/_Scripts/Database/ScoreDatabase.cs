using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreDatabase
{

    public static void save(string name, float score)
    {
        saveTask(name, score);
    }

    static void saveTask(string name, float score)
    {
        string url = Environment.DatabaseURL + name + ".json";
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            //Do nothing
        }
        try
        {
            User dluser = JsonUtility.FromJson<User>(www.text);
            if (dluser.userscore < score)
            {
                RestClient.Put(Environment.DatabaseURL + name + ".json", new User(name, score));
            }
        }
        catch
        {
            RestClient.Put(Environment.DatabaseURL + name + ".json", new User(name, score));
        }
    }

}
