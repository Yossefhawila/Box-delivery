using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float price;
    public bool Holded;   
 
    public void SELL()
    {
        BoxHolder BH =transform.parent.GetComponent<BoxHolder>();
        if( BH != null)
        {
            BH.HaveBox = false;
        }
        GameManager.instance.PlayerCoins += price;
        GameManager.instance.UpdateWhatHappen();
        Destroy(gameObject);
    }
}
