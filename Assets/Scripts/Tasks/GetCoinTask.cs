using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoinTask : MyTask
{
    [SerializeField]
    float NeededCoins = 200;
    public override bool GetTaskStatus()
    {
        if (NeededCoins <= GameManager.instance.PlayerCoins)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
