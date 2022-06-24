using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerTask : MyTask
{
    [SerializeField]
    float spawnersNeeded = 2;
    public override bool GetTaskStatus()
    {
        if (spawnersNeeded <= GameManager.instance.SpawnersCount)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

}
