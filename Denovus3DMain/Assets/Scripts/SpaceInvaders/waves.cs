using UnityEngine;
using UnityEngine.UI;

public class waves : MonoBehaviour
{
    public int globalDifficulty = 1;

    // Types of UFO
    public GameObject UFO_Index;
    public GameObject UFO_Middle;
    public GameObject UFO_Ring;
    public GameObject UFO_Pinky;
    private BTManager manager;

    public int UFOsDestroyed = 0;
    private int indexCount;
    private int middleCount;
    private int ringCount;
    private int pinkyCount;
    private int lastRandomNum = 0;
    private float clock = 0f;
    public int targetNumber;
    private bool finalCheck;
    private float finalPinchDelay = 2f;
    public Text text;
    public Text clockText;
    public Text congratsPinchText;

    //For testing
    [Range(0f, 70f)]
    public int currentIndexValue = 0;
    [Range(0f, 70f)]
    public int currentMiddleValue = 0;
    [Range(0f, 70f)]
    public int currentRingValue = 0;
    [Range(0f, 70f)]
    public int currentPinkyValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        globalDifficulty = PlayerPrefs.GetInt("Difficulty", 1);
        targetNumber = globalDifficulty * 20;
        InvokeRepeating("makeUFO", 0f, 3f);
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (!finalCheck)
            clock += Time.deltaTime;

        clockText.text = Mathf.Floor(clock).ToString();

        text.text = "UFOs Destroyed\n" + UFOsDestroyed + "/" + targetNumber;

        if (UFOsDestroyed == targetNumber)
        {
            CancelInvoke("makeUFO");
            text.text = "Congratulations!";

            if (!finalCheck)
            {
                GameObject.Find("FinalGloveValuesUI").GetComponent<FinalGloveValues>().UpdateValues();
                //GameObject.Find("SAVELOAD").GetComponent<SaveInvaders>().SaveInvaderValues((int)clock);
                congratsPinchText.transform.Translate(0f, 3.7f, 0f);
                FindObjectOfType<AudioManager>().Play("Victory");
                finalCheck = true;
            }

            manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

            if (finalPinchDelay >= 0f)
            {
                finalPinchDelay -= Time.deltaTime;
            }

            else if (manager.sensorValue[0] > TriggerValues.forceTrigger ||
                     manager.sensorValue[1] > TriggerValues.forceTrigger ||
                     manager.sensorValue[2] > TriggerValues.forceTrigger ||
                     manager.sensorValue[3] > TriggerValues.forceTrigger ||
                     manager.sensorValue[4] > TriggerValues.forceTrigger)
            {
                GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().SwitchExerciseToMain();
            }

            //enabled = false;
        }
    }

    public void makeUFO()
    {
        int rand = Random.Range(1, 5);
        while (lastRandomNum == rand)
            rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                Instantiate(UFO_Index);
                break;
            case 2:
                Instantiate(UFO_Middle);
                break;
            case 3:
                Instantiate(UFO_Ring);
                break;
            case 4:
                Instantiate(UFO_Pinky);
                break;
            default:
                break;
        }

        lastRandomNum = rand;
    }

    void addToUFOCount()
    {
       indexCount = UFO_Index.GetComponent<ButtonListener>().UFOCount;
       middleCount = UFO_Middle.GetComponent<ButtonListener>().UFOCount;
       ringCount = UFO_Ring.GetComponent<ButtonListener>().UFOCount;
       pinkyCount = UFO_Pinky.GetComponent<ButtonListener>().UFOCount;
       UFOsDestroyed = indexCount + middleCount + ringCount + pinkyCount;
    }
}

