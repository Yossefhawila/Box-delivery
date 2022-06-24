using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public float price;
    public float amountinclick=1;
    public float priceInclick;
    [HideInInspector]
    public bool Holded;

    private void Start()
    {
        SetPriceCanvas();
    }
    public void SetPriceCanvas()
    {
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = GameManager.getMoneyText(price + GameManager.instance.InGameMoneyPerBox + priceInclick) + "$";
    }
    public void SELL()
    {
        BoxHolder BH =transform.parent.GetComponent<BoxHolder>();
        if( BH != null)
        {
            BH.HaveBox = false;
        }
        GameManager.instance.PlayerCoins += price+GameManager.instance.InGameMoneyPerBox+ priceInclick;
        GameManager.instance.UpdateWhatHappen();
        GameObject opjP = Instantiate(GameManager.instance.Moneyp);
        Destroy(opjP,3);
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        priceInclick += amountinclick;
        SetPriceCanvas();
    }
}
