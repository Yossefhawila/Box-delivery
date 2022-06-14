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

    protected virtual void move(float Horizontal,float Vertical, float speed)
    {
        animator.SetFloat("Vertical",Vertical);
        animator.SetFloat("Horizontal", Horizontal);
        rigidbody.velocity = new Vector2(Horizontal,Vertical)* (ViSpeed+speedInGame);
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
        if (!HaveBox)
        {
            TrackCenter();
            Track(GameManager.instance.StoragePlace);
        }
        else
        {
            Track(GameManager.instance.StoragePlace);
        }
    }
    public void CancelCurrentTrack()
    {
        Tasks.RemoveAt(0);
        if (Tasks.Count > 0)
        {
            GoPos(Tasks[0]);
        }
    }
    public void TrackCenter()
    {
        
        Track(GameManager.instance.CenterPlace);
    }
    public void TrackSell()
    {
        TrackStorage();
        TrackCenter();
        Track(GameManager.instance.SellPlace);
    }
  
    public  void Track(Vector2 pos)
    {
        speedInGame = GameManager.instance.CarSpeed;
        if (Tasks.Count<1)
        {
            audioSource.PlayOneShot(GameManager.instance.CarStart);
            audioSource.clip = CarSound;
            audioSource.PlayDelayed(0.8f);
            audioSource.loop = true;
            Tasks.Add(pos);
            StartCoroutine(GoPos(Tasks[0]));
            
        }
        else 
        {
            if (!Tasks.Contains(pos))
            {
                Tasks.Add(pos);
            }
        }
    }

    public IEnumerator GoPos(Vector2 pos)
    {
        Vector2 dir;
        yield return new WaitForSeconds(1.2f);
        while (true)
        {
           
            dir = -new Vector2(transform.position.x - pos.x, transform.position.y - pos.y).normalized;
            move(dir.x, dir.y, speed);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            if (Vector2.Distance(pos, transform.position) <= 0.5)
            {
               
                Tasks.RemoveAt(0);
                if (Tasks.Count > 0)
                {
                    pos = Tasks[0];
                }
                else
                {
                    audioSource.loop = false;
                    audioSource.clip = GameManager.instance.CarStop;
                    audioSource.Play();
                    move(0, 0, 0);
                    break;
                }
            }
        }
      
    }
}
