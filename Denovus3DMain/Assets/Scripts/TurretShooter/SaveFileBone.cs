using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveFileBone 
{
    public int[] enemiesKilled = new int[7]; //tracked variables

    public SaveFileBone(int[] t)
    {
        enemiesKilled = t;
    }
}
