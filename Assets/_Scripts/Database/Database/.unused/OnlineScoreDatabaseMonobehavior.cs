using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineScoreDatabaseMonobehavior : MonoBehaviour
{

    public void save(string name, float score)
    {
        StartCoroutine(saveTask(name, score));
    }

    IEnumerator saveTask(string name, float score)
    {
        string url = Environment.DatabaseURL + name + ".json";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError)
        {
            Debug.LogError("Network error here");
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
