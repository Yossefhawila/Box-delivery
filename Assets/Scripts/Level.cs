using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public bool opened;
    public int levelIndex=0;
    public Level Next;
    public void ClickMethold()
    {
        if (opened)
        {
            MainGameManager.instance.GameLevelLoader(levelIndex);
        }
    }
    public static void GetOpenLevels(Level levelOne)
    {
        if(MainGameManager.instance.PlayerLevel==0)
        {
            MainGameManager.instance.PlayerLevel = 1;
        }
        Level CurrentLevel = levelOne;
        while (true)
        {
            if(CurrentLevel.levelIndex <= MainGameManager.instance.PlayerLevel)
            {
                CurrentLevel.GetComponent<Button>().interactable = true;
                if (CurrentLevel.Next!=null) 
                {
                    CurrentLevel = CurrentLevel.Next;
                    continue;
                }
            }
            break;
            
        }
    }
}
