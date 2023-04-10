using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;

public class SaveSensors : MonoBehaviour
{
    [HideInInspector]
    public List<int> thumbForceSensorValues, indexForceSensorValues, middleForceSensorValues, ringForceSensorValues, pinkyForceSensorValues = new List<int>();
    public List<int> thumbFlexSensorValues, indexFlexSensorValues, middleFlexSensorValues, ringFlexSensorValues, pinkyFlexSensorValues = new List<int>();
    private bool savingValues = true;
    private int forceThreshold, thumbFlexThreshold, indexFlexThreshold, middleFlexThreshold, ringFlexThreshold, pinkyFlexThreshold;
    private int thumbFlexMax, indexFlexMax, middleFlexMax, ringFlexMax, pinkyFlexMax;

    public enum Exercise
    {
        Space, Stone, Fish, Bone
    };

    public Exercise exercise;
    private BTManager manager;
    [HideInInspector]
    public int avgThumbForce, avgIndexForce, avgMiddleForce, avgRingForce, avgPinkyForce;
    [HideInInspector]
    public int maxThumbForce, maxIndexForce, maxMiddleForce, maxRingForce, maxPinkyForce;

    void Start()
    {
        manager = FindObjectOfType<BTManager>();
        forceThreshold = TriggerValues.forceTrigger;
        thumbFlexThreshold = TriggerValues.thumbFlexTrigger;
        indexFlexThreshold = TriggerValues.indexFlexTrigger;
        middleFlexThreshold = TriggerValues.middleFlexTrigger;
        ringFlexThreshold = TriggerValues.ringFlexTrigger;
        pinkyFlexThreshold = TriggerValues.pinkyFlexTrigger;
        thumbFlexMax = TriggerValues.thumbFlexMax;
        indexFlexMax = TriggerValues.indexFlexMax;
        middleFlexMax = TriggerValues.middleFlexMax;
        ringFlexMax = TriggerValues.ringFlexMax;
        pinkyFlexMax = TriggerValues.pinkyFlexMax;
        if (exercise == Exercise.Stone)
        {
            if(PlayerPrefs.GetInt("Squeeze",1) == 2)
            {
                exercise = Exercise.Bone;
            }
        }
        InvokeRepeating("SaveSensorValues", 0f, 0.05f);
    }

    public void toggle()
    {
        savingValues = !savingValues;
    }

    void SaveSensorValues()
    {
        if (savingValues)
        {
            switch (exercise)
            {
                case Exercise.Space: //Force
                    if (manager.sensorValue[0] > forceThreshold)
                        thumbForceSensorValues.Add(manager.sensorValue[0]);
                    if (manager.sensorValue[1] > forceThreshold)
                        indexForceSensorValues.Add(manager.sensorValue[1]);
                    if (manager.sensorValue[2] > forceThreshold)
                        middleForceSensorValues.Add(manager.sensorValue[2]);
                    if (manager.sensorValue[3] > forceThreshold)
                        ringForceSensorValues.Add(manager.sensorValue[3]);
                    if (manager.sensorValue[4] > forceThreshold)
                        pinkyForceSensorValues.Add(manager.sensorValue[4]);
                    break;
                case Exercise.Stone: //Force
                    if (manager.sensorValue[2] > forceThreshold)
                        middleForceSensorValues.Add(manager.sensorValue[2]);
                    break;
                case Exercise.Fish: //Flex
                    if (manager.sensorValue[5] <= thumbFlexThreshold && manager.sensorValue[5] >= thumbFlexMax)
                        thumbFlexSensorValues.Add(manager.sensorValue[5]);
                    if (manager.sensorValue[6] <= indexFlexThreshold && manager.sensorValue[6] >= indexFlexMax)
                        indexFlexSensorValues.Add(manager.sensorValue[6]);
                    if (manager.sensorValue[7] <= middleFlexThreshold && manager.sensorValue[7] >= middleFlexMax)
                        middleFlexSensorValues.Add(manager.sensorValue[7]);
                    if (manager.sensorValue[8] <= ringFlexThreshold && manager.sensorValue[8] >= ringFlexMax)
                        ringFlexSensorValues.Add(manager.sensorValue[8]);
                    if (manager.sensorValue[9] <= pinkyFlexThreshold && manager.sensorValue[9] >= pinkyFlexMax)
                        pinkyFlexSensorValues.Add(manager.sensorValue[9]);
                    break;
                case Exercise.Bone: //Force
                    if (manager.sensorValue[0] > forceThreshold)
                        thumbForceSensorValues.Add(manager.sensorValue[0]);
                    if (manager.sensorValue[1] > forceThreshold)
                        indexForceSensorValues.Add(manager.sensorValue[1]);
                    if (manager.sensorValue[2] > forceThreshold)
                        middleForceSensorValues.Add(manager.sensorValue[2]);
                    if (manager.sensorValue[3] > forceThreshold)
                        ringForceSensorValues.Add(manager.sensorValue[3]);
                    if (manager.sensorValue[4] > forceThreshold)
                        pinkyForceSensorValues.Add(manager.sensorValue[4]);
                    break;
                default:
                    break;
            }
        }
    }

    public float[] GetSensorValue()
    {
        switch(exercise)
        {
            case Exercise.Space: //Force
                float[] values = new float[5];
                values[0] = returnAvg(thumbForceSensorValues);
                values[1] = returnAvg(indexForceSensorValues);
                values[2] = returnAvg(middleForceSensorValues);
                values[3] = returnAvg(ringForceSensorValues);
                values[4] = returnAvg(pinkyForceSensorValues);
                return values;
            case Exercise.Stone: //Force
                float[] values1 = new float[1];
                values1[0] = returnAvg(middleForceSensorValues);
                return values1;
            case Exercise.Fish: //Flex
                float[] values2 = new float[5];
                values2[0] = returnAvg(thumbFlexSensorValues);
                values2[1] = returnAvg(indexFlexSensorValues);
                values2[2] = returnAvg(middleFlexSensorValues);
                values2[3] = returnAvg(ringFlexSensorValues);
                values2[4] = returnAvg(pinkyFlexSensorValues);
                return values2;
            case Exercise.Bone: //Force
                float[] values3 = new float[5];
                values3[0] = returnAvg(thumbForceSensorValues);
                values3[1] = returnAvg(indexForceSensorValues);
                values3[2] = returnAvg(middleForceSensorValues);
                values3[3] = returnAvg(ringForceSensorValues);
                values3[4] = returnAvg(pinkyForceSensorValues);
                return values3;
            default:
                return new float[] { 0f };
        }
    }

    private float returnAvg(List<int> arr)
    {
        float total = 0;
        foreach(int i in arr)
            total += i;
        return total / arr.Count;
    }
}
