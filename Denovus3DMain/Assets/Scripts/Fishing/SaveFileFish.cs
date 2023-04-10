using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveFileFish
{
    public float[] fishTime = new float[7];
    public SaveFileFish(float[] fish)
    {
        fishTime = fish;
    }

}
