
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using System;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;
    public int UFOCount;
    public UFO myUFO;
    public BTManager blueTeeth;
    public float pinkyPinch;
    public float indexPinch;
    public float middlePinch;
    public float ringPinch;
    public int currentUFO = 0;
  
    public static float pinchInd = 100f; //neutral 370; // lowest 150
    public static float pinchMid = 100f; //neutral 470 // lowest 190
    public static float pinchRing = 100f; //400 neutral full = 150;
    public static float pinchPink = 100f; // neural 400, full 150
    public static float[] vals = { 0f, pinchInd, pinchMid, pinchRing, pinchPink };
    // Start is called before the first frame update
    void Start()
    {
        //vals = new float { 0f, pinchInd, pinchMid, pinchRing, pinchPink };
        UFOCount = 0;
        getName();
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
        GameObject holder = GameObject.FindGameObjectWithTag("bluetooth");
        blueTeeth = holder.GetComponent<BTManager>();

    }

    void InitiateEvent(InteractableStateArgs state)
    {
        getFingerReadings();
        if (state.NewInteractableState == InteractableState.ProximityState)
            proximityEvent.Invoke();
        else if (state.NewInteractableState == InteractableState.ContactState)
        {

            contactEvent.Invoke();
            if (blueTeeth.sensorValue[currentUFO] >= vals[currentUFO])
            {
                //myUFO.health -= 1;
                //if (myUFO.health <= 0)
                //{
                    //UFOCount++;
                    //Destroy(this);
                    //actionEvent.Invoke();
                //}
                //actionEvent.Invoke();
            }
        }
        else if (state.NewInteractableState == InteractableState.ActionState)
        {
            if (blueTeeth.sensorValue[currentUFO] >= vals[currentUFO])
            {
                /*myUFO.health -= 1;
                if (myUFO.health <= 0)
                {
                    UFOCount++;
                    DestroyIt();
                    actionEvent.Invoke();
                }*/
            }
        }
        else
            defaultEvent.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        getFingerReadings();
    }

    public void getFingerReadings()
    {
        blueTeeth.UpdateBluetooth();
        indexPinch = blueTeeth.sensorValue[1];
        middlePinch = blueTeeth.sensorValue[2];
        ringPinch = blueTeeth.sensorValue[3];
        pinkyPinch = blueTeeth.sensorValue[4];
        
    }

    void getName()
    {
        if (this.name =="UFO_Index")
        {
            currentUFO = 1;
        }
        else if (this.name == "UFO_Middle")
        {
            currentUFO = 2;
        }
        else if (this.name == "UFO_Ring")
        {
            currentUFO = 3;
        }
        else if (this.name == "UFO_Pinky")
        {
            currentUFO = 4;
        }
    }
    void DestroyIt()
    {
        Destroy(this);
    }
}



