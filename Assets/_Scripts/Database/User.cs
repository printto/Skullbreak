using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User {

    public string username;
    public float userscore;

    public User(string name, float score)
    {
        username = name;
        userscore = score;
    }

}
