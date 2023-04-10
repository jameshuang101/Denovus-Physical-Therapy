using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialMenu : MonoBehaviour
{
    public Canvas tutorial1;
    public Canvas tutorial2;
    public Canvas tutorial3;
    public Canvas tutorial4;
    public Canvas passthrough;

    private float timer = 15f;
    private int tutorialNumber;
    private int gloveTriggerValue;
    private bool squeeze;
    private int squeezeValue;
    private bool passthroughShown;

    // Start is called before the first frame update
    void Start()
    {
        gloveTriggerValue = TriggerValues.forceTrigger;

        //PlayerPrefs.SetInt("Squeeze", 1);
        squeezeValue = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;

        // Initially show tutorial 1
        if (squeeze)
        {
            tutorial2.transform.Translate(0f, -8f, 0f);
            tutorial3.transform.Translate(0f, -8f, 0f);
            tutorial4.transform.Translate(0f, -8f, 0f);
            passthrough.transform.Translate(0f, 8f, 0f);
            passthroughShown = true;
            tutorialNumber = 1;
        }
        // Initially show tutorial 3
        else
        {
            tutorial1.transform.Translate(0f, -8f, 0f);
            tutorial2.transform.Translate(0f, -8f, 0f);
            tutorial4.transform.Translate(0f, -8f, 0f);
            tutorialNumber = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0f)
        {
            if (squeeze && timer <= 7f)
            {
                if (passthroughShown)
                {
                    passthrough.transform.Translate(0f, -8f, 0f);
                    passthroughShown = false;
                }
            }

            timer -= Time.deltaTime;
        }
        else
        {
            if (squeeze)
            {
                if (tutorialNumber == 1)
                {
                    tutorial1.transform.Translate(0f, -8f, 0f);
                    tutorial2.transform.Translate(0f, 8f, 0f);
                    tutorialNumber = 2;
                    timer = 15f;
                }
                else if (tutorialNumber == 2)
                {
                    tutorial1.transform.Translate(0f, 8f, 0f);
                    tutorial2.transform.Translate(0f, -8f, 0f);
                    tutorialNumber = 1;
                    timer = 15f;
                }
            }
            else
            {
                if (tutorialNumber == 3)
                {
                    tutorial3.transform.Translate(0f, -8f, 0f);
                    tutorial4.transform.Translate(0f, 8f, 0f);
                    tutorialNumber = 4;
                    timer = 15f;
                }
                else if (tutorialNumber == 4)
                {
                    tutorial3.transform.Translate(0f, 8f, 0f);
                    tutorial4.transform.Translate(0f, -8f, 0f);
                    tutorialNumber = 3;
                    timer = 15f;
                }
            }
        }

        BTManager manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

        if (manager.sensorValue[0] > gloveTriggerValue || 
            manager.sensorValue[1] > gloveTriggerValue || 
            manager.sensorValue[2] > gloveTriggerValue || 
            manager.sensorValue[3] > gloveTriggerValue || 
            manager.sensorValue[4] > gloveTriggerValue)
        {
            SceneManager.LoadScene("DenovusLoadingScreen");     // Load loading scene
        }
    }

    
}
