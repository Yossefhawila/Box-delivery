using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;
    public static bool IsGameSoundEnabled=false;
    public bool FirstGameOpen = false;
    private SaveGame SelectedSaveGame;
    AudioSource audioSource;
    
    public int CurrentSaveGameIndex;
    public string PlayerName;
    public string SaveId;


    //audiosorce

    public AudioClip Click;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource =GetComponent<AudioSource>();
        instance = this;
    }
    public void GameStartButton(Button SaveGamesButton)
    {
        
        CanvasOfSaveGameMenu.SetActive(true);
        CanvasOfSaveGameMenu.SetActive(false);

        if (string.IsNullOrEmpty(PlayerName))
        {
           
            SaveGamesButton.onClick.Invoke();
        }
        else
        {
            MainMenu.SetActive(false);
            LevelsCanvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameSound(bool IsSoundEnabeld)
    {
        IsGameSoundEnabled=IsSoundEnabeld;
    }
    public void SetIndexOfSelectedSaveGame(SaveGame SaveGame)
    {
      SelectedSaveGame=SaveGame;
    }


    
    public void CreateSaveGame(Text textOfName)
    {
        if (textOfName.text.Trim().Length > 0)
        {
            
           this.PlayerName=textOfName.text;
            if (string.IsNullOrEmpty(SelectedSaveGame.TextOfSaveGame.text))
            {
                
                SaveId = PlayerName + SelectedSaveGame.IndexOfSaveGame;
                CurrentSaveGameIndex = SelectedSaveGame.IndexOfSaveGame;
                SelectedSaveGame.PlusImage.enabled = false;
                SelectedSaveGame.TextOfSaveGame.text = PlayerName;
                SelectedSaveGame.PlayerName = PlayerName;
                SelectedSaveGame.SaveId = SaveId;
                SaveGame.GetSelected(SelectedSaveGame);
                PlayerPrefs.SetString("Save"+SelectedSaveGame.IndexOfSaveGame, PlayerName);
                MainMenu.SetActive(true);
                CanvasOfCreate.SetActive(false);

                Debug.Log(PlayerName);
            }
        }
        else
        {
            SelectedSaveGame.GetComponent<Button>().onClick.Invoke();
        }

    }
    public void DeleteAllSaveData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void GameLevelLoader(int Level)
    {
        SceneManager.LoadScene(Level);
    }
    public GameObject MainMenu;
    public GameObject LevelsCanvas;
    public GameObject CanvasOfCreate;
    public GameObject CanvasOfSaveGameMenu;
    public void ShowCreateSaveGame(SaveGame saveGame)
    {
        if (string.IsNullOrEmpty(saveGame.TextOfSaveGame.text))
        {
            SetIndexOfSelectedSaveGame(saveGame);
            CanvasOfCreate.SetActive(true);
            CanvasOfSaveGameMenu.SetActive(false);
            audioSource.PlayOneShot(Click);
        }
        else
        {
            SaveGame.GetSelected(saveGame);
        }

    }



}
