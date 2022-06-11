using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGen : MonoBehaviour
{
    [SerializeField]
    private GameObject Box;
    [SerializeField]
    private float price = 10;
    [SerializeField]
    private float GenSpeed=1;

    public bool HaveGenBox;
    
    private void Start()
    {
        StartCoroutine(GenBehave());
    }
    IEnumerator GenBehave()
    {
        while (true)
        {

            yield return new WaitForSeconds(GameManager.instance.SecToOfGen*GenSpeed);
            GenBox();
        }
    }


    public void GenBox()
    {
        if (!HaveGenBox)
        {
            GameObject BOX =Instantiate(Box,transform);
            BOX.GetComponent<Box>().price = price;
            Box.transform.localPosition = new Vector3(0, 0, -5);
            GameManager.instance.Boxes.Add(transform.position);
            GameManager.instance.AskForACar(BOX.transform.position);
            HaveGenBox = true;
        }
        else
        {
            GameManager.instance.AskForACar(transform.position);
        }
    }


}
