using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using OculusSampleFramework;
using UnityEngine.Events;


public class CalibrationPanelHandler : MonoBehaviour
{
    private int counter = 0;
    public Text prompt;
    public GameObject billboardCanvas;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public GameObject table;
    public Vector3 leftHandPosition;
    public Vector3 rightHandPosition;
    public GameObject panelButton;
    public Text debugText;



    public void changePrompt()
    {
        if (counter == 0)
        {
            prompt.text = "Place your left palm on the left corner of your table until the timer reaches 0.\nClick to start ->";
            debugText.text = "Flag = 1";
            counter++;
        }

        else if (counter == 1)
        {
            //debugText.text = "Flag = 2";
            debugText.text = "panelButton off";
            billboardCanvas.GetComponent<CreateCountdownTimer>().CreateTimer();
            StartCoroutine(LeftHandWait());
        }

        else if (counter == 2)
        {
            debugText.text = "Flag = 3";
            prompt.text = "Place your right palm on the right corner of your table until the timer reaches 0.\nClick to start ->";
            counter++;
        }

        else if (counter == 3)
        {
            //debugText.text = "Flag = 4";
            debugText.text = "panelButton off";
            billboardCanvas.GetComponent<CreateCountdownTimer>().CreateTimer();
            StartCoroutine(RightHandWait());
        }
    }

    IEnumerator LeftHandWait()
    {
        yield return new WaitForSecondsRealtime(5);
        leftHandPosition = leftHandTransform.position;
        counter++;
        changePrompt();
        debugText.text = "panelButton on";
    }

    IEnumerator RightHandWait()
    {
        yield return new WaitForSecondsRealtime(5);
        rightHandPosition = rightHandTransform.position;
        prompt.text = "Loading Denovus VR Stroke Therapy. Please wait...";
        table.GetComponent<ScaleTable>().ScalerPressed(leftHandPosition, rightHandPosition);
        counter = 0;
        changePrompt();
        debugText.text = "panelButton on";
    }
}