using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMovement : MonoBehaviour
{
    private Vector2 touchPostion=Vector2.zero;
    [SerializeField]
    private float speed = 0.5f;
    private void Update()
    {
        GetInput();
        if (Input.touchCount == 1)
        {
            move();
        }
    }
    private void GetInput()
    {
        if(Input.touchCount == 1)
        {
            if (touchPostion == Vector2.zero)
            {
                touchPostion = Input.GetTouch(0).position;
                
            }
        }
        else
        {
            touchPostion = Vector2.zero;
        }
    }
    private void move()
    {
        transform.Translate(MovementDir()*Time.fixedDeltaTime*speed);
    }
    private Vector2 MovementDir()
    {
        Vector2 dir = new Vector2(Input.touches[0].position.x-touchPostion.x,Input.touches[0].position.y-touchPostion.y);
        dir = dir.normalized;
        return -dir;
    }
}
