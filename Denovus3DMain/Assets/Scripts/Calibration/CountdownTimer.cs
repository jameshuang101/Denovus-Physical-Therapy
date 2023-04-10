using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText;
    public float timeRemaining = 5;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "5";
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            int seconds = (int)(timeRemaining % 60);
            timerText.text = "" + seconds;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
