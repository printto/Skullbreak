using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{

    public string username;
    //                      tutorial s1 s2 s3 s4 s5 s6 s7 s8 s9 s10 ext
    public float[] scores = {  0,     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0 };

    public User(string name)
    {
        username = name;
    }

    public float GetScore(StageEnum stage) 
    {
        return scores[Array.IndexOf(Enum.GetValues(stage.GetType()), stage)];
    }

    public void SetName(string name)
    {
        username = name;
    }

    public bool CommitScore(float score, StageEnum stage)
    {
        if (scores[Array.IndexOf(Enum.GetValues(typeof(StageEnum)), stage)] < score)
        {
            scores[Array.IndexOf(Enum.GetValues(typeof(StageEnum)), stage)] = score;
            return true;
        }
        return false;
    }

}
