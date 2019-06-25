using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User {

    public string username;
    public float userscore = 0;

    public User(string name, float score)
    {
        username = name;
        userscore = score;
    }

    public virtual void OnLoad(GameObject gameObject)
    {

    }

    public void SetName (string name)
    {
        username = name;
    }

    public bool CommitScore (float score)
    {
        if (userscore < score)
        {
            userscore = score;
            return true;
        }
        return false;
    }

}
