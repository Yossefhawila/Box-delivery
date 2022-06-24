using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    public float speed=5;
    public float ViSpeed=5;
    public List<Vector2> Tasks = new List<Vector2>();
    protected AudioSource audioSource;
    [SerializeField]
    protected AudioClip CarSound;

    public float speedInGame;

    //car
    public bool HaveBox;


    public void GoToRandomBox()
    {
        if (GameManager.instance.Boxes.Count > 0)
        {
            TrackStorage();
            Track(GameManager.instance.Boxes[Random.Range(0, GameManager.instance.Boxes.Count)]);
        }
        else if (Tasks.Count <= 0)
        {
            Invoke("GoToRandomBox", 1f);
        }
    }
    protected virtual void move(float Horizontal,float Vertical, float speed)
    {
        animator.SetFloat("Vertical",Vertical);
        animator.SetFloat("Horizontal", Horizontal);
        rigidbody.velocity = new Vector2(Horizontal, Vertical) * (ViSpeed + speedInGame);
    }

    protected virtual void takeBox()
    {

    }
    protected void sellBox()
    {

    }
    private void Awake()
    {
        ViSpeed = speed;
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    public void TrackStorage()
    {
        
        Track(GameManager.instance.StoragePlace);
    }
    public void CancelCurrentTrack()
    {
        Tasks.RemoveAt(0);
        if (Tasks.Count <= 0)
        {
            pos = Tasks[0];
        }
    }
    public void CancelAllTasks()
    {
        Tasks.Clear();
        pos = transform.position;
    }
    public void TrackCenter()
    {
        
        Track(GameManager.instance.CenterPlace);
    }
    public void TrackSell()
    {
        Track(GameManager.instance.SellPlace);
    }
  
    public  void Track(Vector2 pos)
    {
        speedInGame = GameManager.instance.CarSpeed;
        if (Tasks.Count<1)
        {
            if (audioSource.loop == false||audioSource.clip == null)
            {
                audioSource.PlayOneShot(GameManager.instance.CarStart);
                audioSource.clip = CarSound;
                audioSource.PlayDelayed(0.8f);
                audioSource.loop = true;
            }
            Tasks.Add(pos);
            this.pos = pos;
            StartCoroutine(GoPos());
            
        }
        else 
        {
            if (!Tasks.Contains(pos))
            {
                Tasks.Add(pos);
            }
        }
    }
    Vector2 pos;

    public IEnumerator GoPos()
    {
        CancelInvoke("StopCar");
        Vector2 dir;
        yield return new WaitForSeconds(1.2f);
        while (true)
        {

            dir = -new Vector2(transform.position.x - pos.x, transform.position.y - pos.y).normalized;
            move(dir.x, dir.y, speed);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
            if (Vector2.Distance(pos, transform.position) <= 0.5)
            {

                if (Tasks.Count > 0)
                {
                    Tasks.RemoveAt(0);
                }
                if (Tasks.Count > 0)
                {
                    pos = Tasks[0];
                }
                else
                {
                    Invoke("StopCar",0.5f);
                    GoToRandomBox();
                    move(0, 0, 0);
                    break;
                }
            }
        }
      
    }
    public void StopCar()
    {
        audioSource.loop = false;
        audioSource.clip = GameManager.instance.CarStop;
        audioSource.Play();
    }
}
