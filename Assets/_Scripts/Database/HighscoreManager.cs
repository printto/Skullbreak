using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager {

	public static bool UpdateHighscore(float score)
    {
        if (SaveManager.Load())
        {
            if (score > UserManager.user.userscore)
            {
                UserManager.user.userscore = score;
                SaveManager.Save(UserManager.user);
                return true;
            }
            return false;
        }
        else
        {
            SaveManager.Save(new User("Unused Player Name", score));
            return true;
        }
    }

    public static float GetHighscore()
    {
        if (SaveManager.Load())
        {
            return UserManager.user.userscore;
        }
        return 0;
    }

}
