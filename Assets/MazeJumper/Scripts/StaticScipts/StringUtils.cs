using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtils
{
    public static string SplitAndTrimSceneName(string sceneName)
    {
        string[] splitArray = sceneName.Split('-');
        string editedSceneName = splitArray[0];
        editedSceneName.Trim();
        return editedSceneName;
    }
}
