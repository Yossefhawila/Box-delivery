using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public double PlayerCoins=0;
    public float CarSpeed = 0;
    public float SecToOfGen=30;
    public static GameManager instance;
    [SerializeField]
    private GameObject ThCar;
    [SerializeField]
    Text MoneyText;
    [SerializeField]
    Button CarButton;
    public List<Vector2> Boxes = new List<Vector2>();
    public List<Cr> Cars = new List<Cr>();

    public AudioSource audioSource;
    //sounds
    public AudioClip Click;
    public AudioClip Buy;
    public AudioClip CarSound;
    public AudioClip CarStart;
    public AudioClip CarStop;
    public AudioClip CarBr;
    public AudioClip CarGetAndThrowItem;

    [SerializeField ]
    private Button CancelBuyButton;
    [SerializeField]
    private Button BuyButton;


    // Start is called before the first frame update
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    private void Start()
    {
        UpdateWhatHappen();
    }
    public void AskForACar(Vector2 Pos)
    {
        bool IsTaskDone=false;
       
        
            foreach (Cr Car in Cars)
            {
                if (Car.Tasks.Count < 1)
                {
                    Car.TrackStorage();
                    Car.Track(Pos);
                    IsTaskDone = true;
                   
                    break;
                }
            }
        
        if (!IsTaskDone&Cars.Count>0)
        {
            int num = Random.Range(0, Cars.Count);
            Cars[num].TrackStorage();
            Cars[num].Track(Pos);
        }
    }
    public void addCar()
    {
        if (PlayerCoins >= 100)
        {
            Instantiate(ThCar, new Vector3(0.3f, -0.8f, -3), Quaternion.identity);
            PlayerCoins -= 100;
            UpdateWhatHappen();
            audioSource.PlayOneShot(CarSound);
        }
    }
    public void UpdateWhatHappen()
    {

        MoneyText.text = PlayerCoins.ToString();
        if(PlayerCoins >= 100)
        {
            CarButton.interactable = true;
        }
        else
        {
            CarButton.interactable = false;

        }
        foreach (MyTask mytask in GetComponents<MyTask>())
        {
            if(mytask.GetTaskStatus())
            {
                continue;
            }
            else
            {
                return;
            }
        }
        PlayerEndTasks();

    }

    private void PlayerEndTasks()
    {
        Debug.Log("end");   
    }
    public void AddCarSpeed()
    {
        if(PlayerCoins >= 400)
        {
            CarSpeed += 6;
            PlayerCoins -= 400;
            CancelBuyButton.onClick.Invoke();
            audioSource.PlayOneShot(CarGetAndThrowItem);
            ItemOneMax();
            UpdateWhatHappen();

        }
    }
    [SerializeField]
    private Button Item1Button;
    [SerializeField]
    private Text Item1Text;

    public void ItemOneMax()
    {
        if (CarSpeed >= 18)
        {
            Item1Button.interactable = false;
            Item1Text.text = "Max";
            
        }
    }
}
