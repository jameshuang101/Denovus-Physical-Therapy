using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFileStone
{
    public float[] time = new float[7]; //tracked variables

    public SaveFileStone(float[] t)
    {
        time = t;
    }
}
