using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchPics : MonoBehaviour
{
    public Canvas tutorial1;
    public Canvas tutorial2;

    private float timer = 10f;
    private int tutorialNumber;
    private int gloveTriggerValue;

    // Start is called before the first frame update
    void Awake()
    {
        gloveTriggerValue = TriggerValues.forceTrigger;

        tutorial2.transform.Translate(0f, -8f, 0f);
        tutorialNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (tutorialNumber == 1)
            {
                tutorial1.transform.Translate(0f, -8f, 0f);
                tutorial2.transform.Translate(0f, 8f, 0f);
                tutorialNumber = 2;
                timer = 10f;
            }
            else if (tutorialNumber == 2)
            {
                tutorial1.transform.Translate(0f, 8f, 0f);
                tutorial2.transform.Translate(0f, -8f, 0f);
                tutorialNumber = 1;
                timer = 10f;
            }
        }

        BTManager manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

        if (manager.sensorValue[1] > gloveTriggerValue || manager.sensorValue[2] > gloveTriggerValue || manager.sensorValue[3] > gloveTriggerValue)
        {
            SceneManager.LoadScene("DenovusLoadingScreen");     // Load loading scene
        }
    }


    //public GameObject pic1;
    //public GameObject pic2;
    //public GameObject pic3;
    //public GameObject pic4;
    //public GameObject pic5;
    //public float timer;
    //public float timeToWait;
    //private int counter;
    //public TextMeshPro textToRender;
    //public bool checkForEnd = false;
    //private int gloveTriggerValue;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    gloveTriggerValue = TriggerValues.forceTrigger;

    //    counter = 0;
    //    pic1.GetComponent<SpriteRenderer>().enabled = true;
    //    pic2.GetComponent<SpriteRenderer>().enabled = false;
    //    pic3.GetComponent<SpriteRenderer>().enabled = false;
    //    pic4.GetComponent<SpriteRenderer>().enabled = false;
    //    pic5.GetComponent<SpriteRenderer>().enabled = false;
    //    //textToRender = GetComponent<TextMeshPro>();
    //    //textToRender.GetComponent<MeshRenderer>().enabled = false;
    //    timeToWait = 5f;
    //    timer = 0f;
    //    checkForEnd = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer>= timeToWait && checkForEnd == false)
    //    {
    //        timer = 0f;
    //        if (counter == 0)
    //        {
    //            pic1.GetComponent<SpriteRenderer>().enabled = false;
    //            pic2.GetComponent<SpriteRenderer>().enabled = true;
    //            counter++;
    //        }
    //        if (counter == 1)
    //        {
    //            pic2.GetComponent<SpriteRenderer>().enabled = false;
    //            pic3.GetComponent<SpriteRenderer>().enabled = true;
    //            counter++;
    //        }
    //        if (counter == 2)
    //        {
    //            pic3.GetComponent<SpriteRenderer>().enabled = false;
    //            pic4.GetComponent<SpriteRenderer>().enabled = true;
    //            counter++;
    //        }
    //        if (counter == 3)
    //        {
    //            pic4.GetComponent<SpriteRenderer>().enabled = false;
    //            pic5.GetComponent<SpriteRenderer>().enabled = true;
    //            counter++;
    //        }
    //        if (counter == 4)
    //        {
    //            pic5.GetComponent<SpriteRenderer>().enabled = false;
    //            //pic2.GetComponent<SpriteRenderer>().enabled = true;
    //            counter++;
    //            checkForEnd = true;
    //        }

    //    }
    //    if (counter ==5)
    //    {
    //        timeToWait -= Time.deltaTime;
    //        //textToRender.GetComponent<MeshRenderer>().enabled = true;
    //        //textToRender.SetText("Starting Game in: \n {1:2} s",timeToWait);
    //        if (timeToWait<=0f)
    //        {
    //            PlayerPrefs.SetString("Scene", "Fish");             // Set pref for Fishing Exercise
    //            SceneManager.LoadScene("DenovusLoadingScreen");     // Load loading scene
    //        }
    //    }

    //    BTManager manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

    //    if (manager.sensorValue[1] > gloveTriggerValue || manager.sensorValue[2] > gloveTriggerValue || manager.sensorValue[3] > gloveTriggerValue)
    //    {
    //        PlayerPrefs.SetString("Scene", "Fish");           // Set pref for Fishing Exercise
    //        SceneManager.LoadScene("DenovusLoadingScreen");     // Load loading scene
    //    }
    //}
}
