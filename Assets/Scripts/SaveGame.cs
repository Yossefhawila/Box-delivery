using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    public int IndexOfSaveGame;
    public Text TextOfSaveGame;
    public Image PlusImage;
    public string SaveId;
    public static SaveGame CurrentSaveGame;
    public Button button;

    public void GetSaveData(int index)
    {
        string SaveId = PlayerPrefs.GetString("save"+index);
        if (!string.IsNullOrEmpty(SaveId))
        {
            MainGameManager.instance.SaveId = SaveId;
            TextOfSaveGame.text = SaveId;
        }
        
    }
    public static void GetSelected(SaveGame saveGame)
    {
        if (CurrentSaveGame != null)
        {
            CurrentSaveGame.button.interactable = true;
        }
        
        saveGame.button.interactable = false;
        CurrentSaveGame = saveGame;
        MainGameManager.instance.SaveId = saveGame.SaveId;
        MainGameManager.instance.CurrentSaveGameIndex = saveGame.IndexOfSaveGame;


    }

}
