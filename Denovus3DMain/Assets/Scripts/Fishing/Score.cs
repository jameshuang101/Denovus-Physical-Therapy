using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();
        // The text displayed will be:
        // The first number is 4 and the 2nd is 6.35 and the 3rd is 4.
    }


    public void setScore(int num)
    {
        textBox.text = "Fish remaining: \n" + num;
    }

    public void setCongrats(int completionTime)
    {
        textBox.text = "Congratulations!\nTime: " + completionTime;
        enabled = false;
    }
}
