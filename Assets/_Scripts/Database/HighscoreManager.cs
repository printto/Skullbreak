using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager {

    public static bool UpdateHighscore(float score, string stage)
    {
        return UpdateHighscore(score, stageNameToStageEnum(stage));
    }

    public static bool UpdateHighscore(float score, StageEnum stage)
    {
        if (SaveManager.Load())
        {
            if (UserManager.user.CommitScore(score, stage))
            {
                SaveManager.Save(UserManager.user);
                return true;
            }
            return false;
        }
        else
        {
            SaveManager.Save(new User("Unused Player Name"));
            return true;
        }
    }

    public static float GetHighscore(string stage)
    {
        return GetHighscore(stageNameToStageEnum(stage));
    }

    public static float GetHighscore(StageEnum stage)
    {
        if (SaveManager.Load())
        {
            return UserManager.user.GetScore(stage);
        }
        return 0;
    }

    static StageEnum stageNameToStageEnum(string stageName)
    {
        switch (stageName)
        {
            case "TutorialLevel":
                return StageEnum.TUTORIAL_STAGE;
            case "GridStageMode":
                return StageEnum.STAGE1;
            default:
                return StageEnum.EXTENDED_RESERVED_SLOT;
        } 
    }

}
