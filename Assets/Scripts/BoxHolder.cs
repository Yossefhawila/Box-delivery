using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHolder : MonoBehaviour
{
    public bool HaveBox;

    public void GetBox(GameObject BOX)
    {
            
            HaveBox = true;
            BOX.transform.parent = transform;
            BOX.transform.localPosition = new Vector3(0, 0, BOX.transform.position.z);
        
    }
}
