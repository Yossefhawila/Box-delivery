using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlace : MonoBehaviour
{
    public float Price = 200;
    public GameObject Boxplace; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.Click);
        if (GameManager.instance.PlayerCoins>=Price)
        {
            Instantiate(Boxplace,transform.position,transform.rotation);
            Destroy(gameObject);
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.CarSound);
        }
    }
}
