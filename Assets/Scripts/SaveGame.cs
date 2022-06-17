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
    private string PlayerName;
    public static SaveGame CurrentSaveGame;
    public Button button;

    private void Awake()
    {
        GetSaveData();
    }
    private void Start()
    {
        GetLastSelected();   
    }
    public void GetSaveData()
    {
        PlayerName = PlayerPrefs.GetString("Save"+IndexOfSaveGame);
        if (!string.IsNullOrEmpty(PlayerName))
        {
            SaveId = PlayerName + IndexOfSaveGame;
            PlusImage.enabled = false;
            TextOfSaveGame.text = PlayerName;
        }
     
        
    }
    public void GetLastSelected()
    {
        if (PlayerPrefs.GetInt("LastSelectedSaveGame") == IndexOfSaveGame)
        {
            if(!string.IsNullOrEmpty(PlayerName))
            {
                GetSelected(this);
            }
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
        PlayerPrefs.SetInt("LastSelectedSaveGame", saveGame.IndexOfSaveGame);


    }

}
