using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public double PlayerCoins=0;
    public float CarSpeed = 0;
    public float SecToOfGen=30;
    public float InGameSpeedOfGen=15;
    public float InGameMoneyPerBox=0;
    public static GameManager instance;
    public GameObject Moneyp;
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
    public AudioClip BackGroundSound;
    public AudioClip BoxSpawn;

    [SerializeField ]
    private Button CancelBuyButton;
    [SerializeField]
    private Button BuyButton;


    public Vector2 SellPlace;
    public Vector2 StoragePlace;
    public Vector2 CenterPlace;
    // Start is called before the first frame update
    private void Awake()
    {
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BackGroundSound;
        audioSource.Play();
        instance = this;
        SellPlace = GameObject.FindGameObjectWithTag("SellPlace").transform.position;
        StoragePlace = GameObject.FindGameObjectWithTag("StoragePlace").transform.position;
        CenterPlace = GameObject.FindGameObjectWithTag("CenterPlace").transform.position;


    }
    public void LoadSceneWithBuildIndex(int BuildINDEX)
    {
        SceneManager.LoadScene(BuildINDEX);
    }
    public void ReloadSence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        SetTimeScale(0);
        UpdateWhatHappen();
    }
    public void SetTimeScale(float Scale = 1) 
    {
        Time.timeScale = Scale;
    }
    public void AskForACar(Vector2 Pos)
    {
        bool IsTaskDone=false;
       
        
            foreach (Cr Car in Cars)
            {
                if (Car.Tasks.Count < 1)
                {
                    
                    Car.Track(Pos);
                    IsTaskDone = true;
                   
                    break;
                }
            }
        
        //if (!IsTaskDone&Cars.Count>0)
        //{
        //    int num = Random.Range(0, Cars.Count);
        //    Cars[num].TrackStorage();
        //    Cars[num].Track(Pos);
        //}
    }
    public void addCar()
    {
        if (PlayerCoins >= 100)
        {
            Instantiate(ThCar, new Vector3(0.3f, -0.8f, -6), Quaternion.identity);
            PlayerCoins -= 100;
            UpdateWhatHappen();
            audioSource.PlayOneShot(CarSound);
        }
    }

    public string getMoneyText(double MOneyhere)
    {
        string sympol = "";

        string[] MoneySympols = { "K", "M", "B", "T", "P", "L", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "L", "N", "O", "Q", "R", "S", "U", "V", "W", "Z" };


        double MoneyVAL = MOneyhere;

        int x = -1;

        while (MoneyVAL > 1000)
        {
            if (MoneyVAL > 1000)
            {
                MoneyVAL /= 1000;
                x += 1;
            }
            if (x >= MoneySympols.Length - 1)
            {
                sympol += "x";
                x = -1;


            }

        }



        if (x == -1)
        {
            return MoneyVAL.ToString("0.#") + new string(sympol.ToCharArray().Reverse().ToArray());
        }
        else
        {
            sympol += MoneySympols[x];
            return MoneyVAL.ToString("0.#") + new string(sympol.ToCharArray().Reverse().ToArray());
        }

    }

    public void UpdateWhatHappen()
    {

        MoneyText.text = getMoneyText(PlayerCoins);
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

    [SerializeField]
    private Text TextTitle;

    [SerializeField]
    private Text TextDisc;

    public void GetCurrentBuyStats(string StatsId)
    {
        Invoke(StatsId,0);
    }
    private string BuyMenuTaskString = "";
    public void SetBuyMenuTaskString(string str)
    {
        BuyMenuTaskString = str;
    }

    public void BuyMeth()
    {
        if (!string.IsNullOrEmpty(BuyMenuTaskString)) 
        {
            Invoke(BuyMenuTaskString,0);
        }
    }

    private float CarSpeedPrice = 50f;
    private float AddCarSpeedAmount = 3;
    public void AddCarSpeed()
    {
        if(PlayerCoins >= CarSpeedPrice)
        {
            CarSpeed += AddCarSpeedAmount;
            PlayerCoins -= CarSpeedPrice;
            CancelBuyButton.onClick.Invoke();
            audioSource.PlayOneShot(CarGetAndThrowItem);
            UpdateWhatHappen();
            CarSpeedPrice *= 3;
            AddCarSpeedAmount *= 2;
            CarSpeedMax();


        }
    }

    public void CarSpeedStats()
    {
        TextTitle.text = "Car speed: "+(CarSpeed+AddCarSpeedAmount);
        TextDisc.text = "Add "+AddCarSpeedAmount+" speed to the car with "+CarSpeedPrice+"$";
    }

    [SerializeField]
    private Button CarSpeedButton;
    [SerializeField]
    private Text CarSpeedText;
    
    public void CarSpeedMax()
    {
        if (CarSpeed >= 18)
        {
            CarSpeedButton.interactable = false;
            CarSpeedText.text = "Max";

        }
        else
        {
            CarSpeedText.text = "+"+AddCarSpeedAmount;
        }

    }
    private float SpawnRatePrice = 50;
    private float SpawnRateReduceAmount = 5;
    public void AddSpawnRate()
    {
        if (PlayerCoins >= SpawnRatePrice)
        {
            InGameSpeedOfGen -= SpawnRateReduceAmount;
            PlayerCoins -= SpawnRatePrice;
            CancelBuyButton.onClick.Invoke();
            audioSource.PlayOneShot(CarGetAndThrowItem);
            UpdateWhatHappen();
            SpawnRatePrice *= 3;
            SpawnRateReduceAmount *= 2;
            SpawnRateMax();

        }
    }

    [SerializeField]
    private Button SpawnRateButton;
    [SerializeField]
    private Text SpawnRateText;

    public void SpawnRateMax()
    {
        if (InGameSpeedOfGen <= 0)
        {
            InGameSpeedOfGen = 0;
            SpawnRateButton.interactable = false;
            SpawnRateText.text = "Max";

        }
        else
        {
            SpawnRateText.text = "-"+SpawnRateReduceAmount;
        }
    }
    public void SpawnRateStats()
    {
        TextTitle.text = "In Game Spawn rate: " +1/(InGameSpeedOfGen-5<0?0:InGameSpeedOfGen-5);
        TextDisc.text = "Reduce spawn time by "+SpawnRateReduceAmount+" with " + SpawnRatePrice + "$";
    }

    private float MoneyPerBoxPrice = 50;
    private float AddMoneyPerBoxAmount = 5;
    public void AddMoneyPerBox()
    {
        if (PlayerCoins >= MoneyPerBoxPrice)
        {
            InGameMoneyPerBox += AddMoneyPerBoxAmount;
            PlayerCoins -= MoneyPerBoxPrice;
            CancelBuyButton.onClick.Invoke();
            audioSource.PlayOneShot(CarGetAndThrowItem);
            UpdateWhatHappen();
            MoneyPerBoxPrice *= 3;
            AddMoneyPerBoxAmount *= 2;
            MoneyPerBoxMax();

        }
    }

    [SerializeField]
    private Button MoneyPerBoxButton;
    [SerializeField]
    private Text MoneyPerBoxText;

    public void MoneyPerBoxMax()
    {
        if (InGameMoneyPerBox >= 100)
        {
            MoneyPerBoxButton.interactable = false;
            MoneyPerBoxText.text = "Max";

        }
        else
        {
            MoneyPerBoxText.text = "+" + AddMoneyPerBoxAmount;
        }
    }
    public void MoneyPerBoxStats()
    {
        TextTitle.text = (InGameMoneyPerBox+AddMoneyPerBoxAmount)+"$ more per box: ";
        TextDisc.text = "Add " + AddMoneyPerBoxAmount + "$ per box more with " + MoneyPerBoxPrice + "$";
    }
}
