using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cr : Controller
{
    float step;
    private void Start()
    {
    
        GameManager.instance.Cars.Add(this);
        if(GameManager.instance.Boxes.Count > 0)
        {
            TrackStorage();
            Track(GameManager.instance.Boxes[0]);
        }
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
                    audioSource.PlayOneShot(GameManager.instance.CarGetAndThrowItem);
                    TrackStorage();
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
                        }
                        GameManager.instance.Boxes.Remove(box.transform.position);
                        box.Holded = true;
                        BH.GetBox(collision.gameObject);
                        audioSource.PlayOneShot(GameManager.instance.CarGetAndThrowItem);
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
                GameManager.instance.audioSource.clip = GameManager.instance.CarBr;
                GameManager.instance.audioSource.PlayDelayed(0.3f);
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
