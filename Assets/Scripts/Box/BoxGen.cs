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
    public bool HaveGenBox;
    
    private void Start()
    {
        GameManager.instance.SpawnersCount+=1;
        Banel = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        SecsText = Banel.GetComponentInChildren<Text>();
        StartCoroutine(GenBehave());
        GameManager.instance.UpdateWhatHappen();
        
    }
    private float waitTime;
    public IEnumerator GenBehave()
    {
        
        waitTime = GameManager.instance.SecToOfGen + GenSpeed + GameManager.instance.InGameSpeedOfGen;
        if (!HaveGenBox)
        {
            StartCoroutine(TextSecLoop(waitTime));
        }
        yield return new WaitForSeconds(0);
      
        

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
        time = 0;
        GenBox();
        Banel.enabled = false;
        SecsText.enabled = false;
    }

    public void GenBox()
    {
        GameObject BOX =Instantiate(Box,transform);
        BOX.GetComponent<Box>().price = price;
        Box.transform.localPosition = new Vector3(0, 0, -5);
        GameManager.instance.Boxes.Add(transform.position);
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.BoxSpawn,0.5f);
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
