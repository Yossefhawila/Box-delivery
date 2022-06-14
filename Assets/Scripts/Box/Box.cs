using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [HideInInspector]
    public float price;
    [HideInInspector]
    public bool Holded;   
 
    public void SELL()
    {
        BoxHolder BH =transform.parent.GetComponent<BoxHolder>();
        if( BH != null)
        {
            BH.HaveBox = false;
        }
        GameManager.instance.PlayerCoins += price+GameManager.instance.InGameMoneyPerBox;
        GameManager.instance.UpdateWhatHappen();
        GameObject opjP = Instantiate(GameManager.instance.Moneyp);
        Destroy(opjP,3);
        Destroy(gameObject);
    }
}
