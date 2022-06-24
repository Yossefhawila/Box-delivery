using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCarTask : MyTask
{
    [SerializeField]
    float NeededCars = 3;
    public override bool GetTaskStatus()
    {
        if (GameManager.instance.Cars.Count>=NeededCars)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
