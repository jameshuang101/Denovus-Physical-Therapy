using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapBehavior : MonoBehaviour
{
    private float[] minSensorValue;
    private float[] maxSensorValue;
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
        maxSensorValue = new float[] { TriggerValues.thumbFlexNeutral, TriggerValues.indexFlexNeutral, TriggerValues.middleFlexNeutral, TriggerValues.ringFlexNeutral, TriggerValues.pinkyFlexNeutral };
        minSensorValue = new float[] { TriggerValues.thumbFlexMax, TriggerValues.indexFlexMax, TriggerValues.middleFlexMax, TriggerValues.ringFlexMax, TriggerValues.pinkyFlexMax};
        hmRenderer = GetComponent<SpriteRenderer>();
        hmRenderer.color = new Color(1, 0, 0, .5f);
        bTManager = FindObjectOfType<BTManager>();
        //hand = rightHand.GetComponent<OVRHand>();

        InvokeRepeating(nameof(UpdateBehavior), 0f, .1f);
    }

    void UpdateBehavior()
    {
        //float colorShiftAmt = ((sensorValue - minSensorValue) / maxSensorValue);
        //hmRenderer.color = new Color(1.0f - colorShiftAmt, 1.0f - colorShiftAmt, 1, 0.5f);
        if (finger == Finger.Pinky)
        {
            sensorValue = bTManager.sensorValue[9];
            if (sensorValue < minSensorValue[4] && sensorValue != -999)
            {
                sensorValue = minSensorValue[4];
            }
            else if (sensorValue > maxSensorValue[4])
            {
                sensorValue = maxSensorValue[4];
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);
            if (sensorValue >= minSensorValue[4] && sensorValue <= maxSensorValue[4] && sensorValue != 0)
                hmRenderer.color = new Color((sensorValue - minSensorValue[4]) / (maxSensorValue[4] - minSensorValue[4]), 1f, (sensorValue - minSensorValue[4]) / (maxSensorValue[4] - minSensorValue[4]), .9f);
                //hmRenderer.color = new Color((maxSensorValue[4] - sensorValue) / (maxSensorValue[4] - minSensorValue[4]), (maxSensorValue[4] - sensorValue) / (maxSensorValue[4] - minSensorValue[4]), 1, 0.5f);
        }
        else if (finger == Finger.Ring)
        {
            sensorValue = bTManager.sensorValue[8];
            if (sensorValue < minSensorValue[3] && sensorValue != -999)
            {
                sensorValue = minSensorValue[3];
            }
            else if (sensorValue > maxSensorValue[3])
            {
                sensorValue = maxSensorValue[3];
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
            if (sensorValue >= minSensorValue[3] && sensorValue <= maxSensorValue[3] && sensorValue != 0)
                hmRenderer.color = new Color((sensorValue - minSensorValue[3]) / (maxSensorValue[3] - minSensorValue[3]), 1f, (sensorValue - minSensorValue[3]) / (maxSensorValue[3] - minSensorValue[3]), .9f);
                //hmRenderer.color = new Color((maxSensorValue[3] - sensorValue) / (maxSensorValue[3] - minSensorValue[3]), (maxSensorValue[3] - sensorValue) / (maxSensorValue[3] - minSensorValue[3]), 1, 0.5f);
        }
        else if (finger == Finger.Middle)
        {
            sensorValue = bTManager.sensorValue[7];
            if (sensorValue < minSensorValue[2] && sensorValue != -999)
            {
                sensorValue = minSensorValue[2];
            }
            else if (sensorValue > maxSensorValue[2])
            {
                sensorValue = maxSensorValue[2];
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
            if (sensorValue >= minSensorValue[2] && sensorValue <= maxSensorValue[2] && sensorValue != 0)
                hmRenderer.color = new Color((sensorValue - minSensorValue[2]) / (maxSensorValue[2] - minSensorValue[2]), 1f, (sensorValue - minSensorValue[2]) / (maxSensorValue[2] - minSensorValue[2]), .9f);
                //hmRenderer.color = new Color((maxSensorValue[2] - sensorValue) / (maxSensorValue[2] - minSensorValue[2]), (maxSensorValue[2] - sensorValue) / (maxSensorValue[2] - minSensorValue[2]), 1, 0.5f);
        }
        else if (finger == Finger.Index)
        {
            sensorValue = bTManager.sensorValue[6];
            if (sensorValue < minSensorValue[1] && sensorValue != -999)
            {
                sensorValue = minSensorValue[1];
            }
            else if (sensorValue > maxSensorValue[1])
            {
                sensorValue = maxSensorValue[1];
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
            if (sensorValue >= minSensorValue[1] && sensorValue <= maxSensorValue[1] && sensorValue != 0)
                hmRenderer.color = new Color((sensorValue - minSensorValue[1]) / (maxSensorValue[1] - minSensorValue[1]), 1f, (sensorValue - minSensorValue[1]) / (maxSensorValue[1] - minSensorValue[1]), .9f);
                //hmRenderer.color = new Color((maxSensorValue[1] - sensorValue) / (maxSensorValue[1] - minSensorValue[1]), (maxSensorValue[1] - sensorValue) / (maxSensorValue[1] - minSensorValue[1]), 1, 0.5f);
        }
        else if (finger == Finger.Thumb)
        {
            sensorValue = bTManager.sensorValue[5];
            if (sensorValue < minSensorValue[0] && sensorValue != -999)
            {
                sensorValue = minSensorValue[0];
            }
            else if (sensorValue > maxSensorValue[0])
            {
                sensorValue = maxSensorValue[0];
            }
            //pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
            if (sensorValue >= minSensorValue[0] && sensorValue <= maxSensorValue[0] && sensorValue != 0)
                hmRenderer.color = new Color((sensorValue - minSensorValue[0]) / (maxSensorValue[0] - minSensorValue[0]), 1f, (sensorValue - minSensorValue[0]) / (maxSensorValue[0] - minSensorValue[0]), .9f);
                //hmRenderer.color = new Color((maxSensorValue[0] - sensorValue) / (maxSensorValue[0] - minSensorValue[0]), (maxSensorValue[0] - sensorValue) / (maxSensorValue[0] - minSensorValue[0]), 1, 0.5f);
        }
        else { }
        
    }
}
