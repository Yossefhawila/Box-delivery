using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static bool IsGameSoundEnabled=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameSound(bool IsSoundEnabeld)
    {
        IsGameSoundEnabled=IsSoundEnabeld;
    }
}
