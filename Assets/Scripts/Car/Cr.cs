using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cr : Controller
{
    float step;
    private void Start()
    {
        if(Tasks.Count > 0)
        {
            Track(transform.position);
        }
    
        GameManager.instance.Cars.Add(this);
        GoToRandomBox();
        GameManager.instance.UpdateWhatHappen();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "sell")
        {
            if (transform.GetChild(0).GetComponent<BoxHolder>().HaveBox)
            {
                Box BX = transform.GetChild(0).GetChild(0).GetComponent<Box>();
                if (BX != null)
                {
                    BX.SELL();
                    HaveBox = false;
                    audioSource.PlayOneShot(GameManager.instance.CarGetAndThrowItem);
                    GoToRandomBox();
                }
            }
        }
        if(collision.tag == "box")
        {
           
           BoxHolder BH=transform.GetChild(0).GetComponent<BoxHolder>();
            if(BH != null)
            {
                if (!BH.HaveBox)
                {
                    Box box =collision.GetComponent<Box>();

                    if (!box.Holded)
                    {
                        BoxGen  BG=collision.transform.parent.GetComponent<BoxGen>();
                        if(BG != null)
                        {
                            BG.HaveGenBox = false;
                            BG.StartCoroutine(BG.GenBehave());
                        }
                        GameManager.instance.Boxes.Remove(box.transform.position);
                        box.Holded = true;
                        HaveBox = true;
                        BH.GetBox(collision.gameObject);
                        audioSource.PlayOneShot(GameManager.instance.CarGetAndThrowItem);
                        CancelAllTasks();
                        TrackStorage();
                        TrackCenter();
                        TrackSell();
                    }
                }
                
            }
        }
        if (collision.tag == "collider2")
        {
            if (Random.Range(0, 4*GameManager.instance.Cars.Count) == 0)
            {
                audioSource.PlayOneShot(GameManager.instance.CarBr);
                
            }
        }
    }
  
  
    bool clicked;
    private void OnMouseDown()
    { 
        ViSpeed += 0.5f;
        CancelInvoke("resetSpeed");
        Invoke("resetSpeed", 0.5f);
    }
    void resetSpeed()
    {
        ViSpeed = speed;
    }
    




}
