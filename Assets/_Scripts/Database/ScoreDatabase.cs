using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDatabase : MonoBehaviour {

    public static void save(string name, float score)
    {
        RestClient.Post(Environment.DatabaseURL + ".json", new User(name, score));
    }

}
