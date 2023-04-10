using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTodayText : MonoBehaviour
{
    public Text golemText, skellyText, spaceText, fishText;
    // Start is called before the first frame update
    void Start()
    {
        int golemVal = (int)PlayerPrefs.GetFloat("GolemTime", 0);
        int skellyVal = PlayerPrefs.GetInt("SkellyScore", 0);
        int spaceVal = (int)PlayerPrefs.GetFloat("SpaceTime", 0);
        int fishVal = (int)PlayerPrefs.GetFloat("FishTime", 0);
        golemText.text = "" + golemVal;
        skellyText.text = "" + skellyVal;
        spaceText.text = "" + spaceVal;
        fishText.text = "" + fishVal;
    }

}
