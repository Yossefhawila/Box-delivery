using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxGen : MonoBehaviour
{
    [SerializeField]
    private GameObject Box;
    [SerializeField]
    private float price = 10;
    [SerializeField]
    private float GenSpeed=1;

    private Image Banel;
    private Text SecsText;
    [HideInInspector]
    public bool HaveGenBox;
    
    private void Start()
    {
        Banel = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        SecsText = Banel.GetComponentInChildren<Text>();
        StartCoroutine(GenBehave());
        InvokeRepeating("AskAcar", 2f,5f);
        
    }
    private float waitTime;
    public IEnumerator GenBehave()
    {
        
        waitTime = GameManager.instance.SecToOfGen + GenSpeed + GameManager.instance.InGameSpeedOfGen;
        if (!HaveGenBox)
        {
            StartCoroutine(TextSecLoop(waitTime));
        }
        yield return new WaitForSeconds(((GameManager.instance.SecToOfGen+GenSpeed+GameManager.instance.InGameSpeedOfGen)<=0.5)? 0.5f: (GameManager.instance.SecToOfGen + GenSpeed + GameManager.instance.InGameSpeedOfGen));
        if (!HaveGenBox)
        {
         
            GenBox();
        }
        

    }

    public IEnumerator TextSecLoop(float time)
    {
        yield return new WaitForSeconds(1f);
        time -= 1;
        Banel.enabled = true;
        SecsText.enabled = true;
        while (time > 0)
        {
            SecsText.text =time.ToString();
            yield return new WaitForSeconds(1);
            time -= 1;
        }
        Banel.enabled = false;
        SecsText.enabled = false;
    }

    public void GenBox()
    {
        GameObject BOX =Instantiate(Box,transform);
        BOX.GetComponent<Box>().price = price;
        Box.transform.localPosition = new Vector3(0, 0, -5);
        GameManager.instance.Boxes.Add(transform.position);
        HaveGenBox = true;
        AskAcar();
        
        
        
    }
    private void AskAcar()
    {
        if (HaveGenBox)
        {
            GameManager.instance.AskForACar(transform.position);
        }
    }

}
