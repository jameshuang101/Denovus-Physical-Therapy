using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapGesture : MonoBehaviour
{
    private float minSensorValue = 0.0F;
    private float maxSensorValue;
    //public GameObject rightHand;
    //private OVRHand hand;
    //private float pinchStrength = 0.0F;
    public enum Finger
    {
        Pinky, Ring, Middle, Index, Thumb
    };
    public Finger finger;
    public float sensorValue = 0.0F;
    private SpriteRenderer hmRenderer;
    private BTManager bTManager;

    void Start()
    {
        maxSensorValue = TriggerValues.forceTriggerHard;
        hmRenderer = GetComponent<SpriteRenderer>();
        hmRenderer.color = new Color(1, 0, 0, 0.5f);
        //hand = rightHand.GetComponent<OVRHand>();
        bTManager = FindObjectOfType<BTManager>();

        InvokeRepeating(nameof(UpdateGesture), 0f, .1f);
    }

    void UpdateGesture()
    {
        //float colorShiftAmt = ((sensorValue - minSensorValue) / maxSensorValue);
        //hmRenderer.color = new Color(1.0f - colorShiftAmt, 1.0f - colorShiftAmt, 1, 0.5f);
        
        if (finger == Finger.Pinky)
        {
            sensorValue = bTManager.sensorValue[4];
            if(sensorValue < minSensorValue)
            {
                sensorValue = minSensorValue;
            }
            else if (sensorValue > maxSensorValue)
            {
                sensorValue = maxSensorValue;
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);
        }
        else if (finger == Finger.Ring)
        {
            sensorValue = bTManager.sensorValue[3];
            if (sensorValue < minSensorValue)
            {
                sensorValue = minSensorValue;
            }
            else if(sensorValue > maxSensorValue)
            {
                sensorValue = maxSensorValue;
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        }
        else if (finger == Finger.Middle)
        {
            sensorValue = bTManager.sensorValue[2];
            if (sensorValue < minSensorValue)
            {
                sensorValue = minSensorValue;
            }
            else if (sensorValue > maxSensorValue)
            {
                sensorValue = maxSensorValue;
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        }
        else if (finger == Finger.Index)
        {
            sensorValue = bTManager.sensorValue[1];
            if (sensorValue < minSensorValue)
            {
                sensorValue = minSensorValue;
            }
            else if (sensorValue > maxSensorValue)
            {
                sensorValue = maxSensorValue;
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        }
        else if (finger == Finger.Thumb)
        {
            sensorValue = bTManager.sensorValue[0];
            if (sensorValue < minSensorValue)
            {
                sensorValue = minSensorValue;
            }
            else if (sensorValue > maxSensorValue)
            {
                sensorValue = maxSensorValue;
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
        }
        if (sensorValue >= minSensorValue && sensorValue <= maxSensorValue && sensorValue != 0)
            hmRenderer.color = new Color(1f - (sensorValue - minSensorValue) / (maxSensorValue - minSensorValue), 1f, 1f - (sensorValue - minSensorValue) / (maxSensorValue - minSensorValue), .9f);
            //hmRenderer.color = new Color((sensorValue - minSensorValue) / (maxSensorValue - minSensorValue), (sensorValue - minSensorValue) / (maxSensorValue - minSensorValue), 1f, .5f);
    }
}
