using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MyTask : MonoBehaviour
{
    [SerializeField]
    protected GameObject Tick;
    [SerializeField]
    protected float DiamondAmount=5;
    public abstract bool GetTaskStatus();
    public virtual void SetTick(bool show=false)
    {
        if (GetTaskStatus()&&show)
        {
            Tick.GetComponent<Image>().enabled = true;
           
        }
        else
        {
            
            Tick.GetComponent<Image>().enabled = false;
        }
    } 
    
}
