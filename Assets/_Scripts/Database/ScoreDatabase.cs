using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreDatabase
{

    static UnityWebRequest www;

    public static void save(string name, float score)
    {
        string url = Environment.DatabaseURL + name + ".json";
        www = UnityWebRequest.Get(url);
        www.SendWebRequest();
        //new Thread(() => saveTask(name, score)).Start();
        saveTask(name, score);
    }

    static void saveTask(string name, float score)
    {
        while (!www.isDone)
        {
            if(www.isHttpError || www.isNetworkError)
            {
                Debug.LogError("Network error here");
                break;
            }
        }
        try
        {
            User dluser = JsonUtility.FromJson<User>(www.downloadHandler.text);
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
