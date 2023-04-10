using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish1;
    private int difficulty;
    [HideInInspector]
    public int thumbTrigger, indexTrigger, middleTrigger, ringTrigger, pinkyTrigger;
    private int triggerOffset = 20;
    private Transform fishingBob;
    [HideInInspector]
    public int numFish;
    public List<Vector3> wayPoints = new List<Vector3>();
    public bool caught = false;
    public float startTime;
    public Score scoreText;
    public BobBehaviour bobBehaviour;
    [HideInInspector]
    public bool finalCheck = false;
    public Text timerText;
    public Text goodJobText;
    private BTManager manager;
    private float finalPinchDelay = 2f;
    private int completionTime;
    public GameObject bob;

    //void Awake()
    //{
    //    PlayerPrefs.SetString("Scene", "Fish");
    //    PlayerPrefs.SetInt("Difficulty", 1);
    //}

    void Start()
    {
        var waypointObjects = GameObject.FindGameObjectsWithTag("Waypoints");
        foreach(GameObject go in waypointObjects)
        {
            wayPoints.Add(go.transform.position);
        }
        PlayerPrefs.SetInt("Difficulty", 1);
        difficulty = PlayerPrefs.GetInt("Difficulty", 2);
        switch(difficulty)
        {
            case 1:
                numFish = 6;
                thumbTrigger = TriggerValues.thumbFlexTrigger;
                indexTrigger = TriggerValues.indexFlexTrigger;
                middleTrigger = TriggerValues.middleFlexTrigger;
                ringTrigger = TriggerValues.ringFlexTrigger;
                pinkyTrigger = TriggerValues.pinkyFlexTrigger;
                break;
            case 2:
                numFish = 9;
                thumbTrigger = (TriggerValues.thumbFlexMax + triggerOffset + TriggerValues.thumbFlexTrigger) / 2;
                indexTrigger = (TriggerValues.indexFlexMax + triggerOffset + TriggerValues.indexFlexTrigger) / 2;
                middleTrigger = (TriggerValues.middleFlexMax + triggerOffset + TriggerValues.middleFlexTrigger) / 2;
                ringTrigger = (TriggerValues.ringFlexMax + triggerOffset + TriggerValues.ringFlexTrigger) / 2;
                pinkyTrigger = (TriggerValues.pinkyFlexMax + triggerOffset + TriggerValues.pinkyFlexTrigger) / 2;
                break;
            case 3:
                numFish = 12;
                thumbTrigger = TriggerValues.thumbFlexMax + triggerOffset;
                indexTrigger = TriggerValues.indexFlexMax + triggerOffset;
                middleTrigger = TriggerValues.middleFlexMax + triggerOffset;
                ringTrigger = TriggerValues.ringFlexMax + triggerOffset;
                pinkyTrigger = TriggerValues.pinkyFlexMax + triggerOffset;
                break;
            default:
                break;
        }

        //Spawn numFish fish at random waypoints
        for(int i = 0; i < numFish; i++)
        {
            int fishRand = UnityEngine.Random.Range(1, 3);
            int rand = UnityEngine.Random.Range(0, wayPoints.Count);
            Vector3 randPosition = wayPoints[rand];
            wayPoints.RemoveAt(rand);
            Instantiate(fish1, randPosition, new Quaternion(0f,0f,0f,0f));
            wayPoints.Add(randPosition);
        }

        scoreText.setScore(numFish);
        fishingBob = GameObject.Find("FishingBob").transform;
        startTime = Time.time;
    }

    void Update()
    {
        if (!finalCheck)
        {
            completionTime = (int)(Time.time - startTime);
            timerText.text = "" + completionTime;
        }

        if(numFish == 0)
        {
            if (!finalCheck)
            {
                GameObject.Find("Table").GetComponent<Table>().MakeTableOpaque();
                GameObject.Find("FinalGloveValuesUI").GetComponent<FinalGloveValues>().UpdateValues();
                FindObjectOfType<AudioManager>().Play("Victory");
                scoreText.setCongrats(completionTime);
                bobBehaviour.endBob();
                //GameObject.Find("SAVELOAD").GetComponent<SaveFish>().SaveFishValues(Time.time - startTime);
                goodJobText.text = "GOOD JOB!\n\nBEND FOR\nMAIN MENU";
                Destroy(bob.gameObject);
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
        }
    }

    public void addBobAsWaypoint()
    {
        wayPoints.Add(fishingBob.position);
    }

    public void removeBobAsWaypoint()
    {
        wayPoints.Remove(fishingBob.position);
    }
}
