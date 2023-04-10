using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFileInvaders
{
    [HideInInspector]
    public int[] aliensDestroyed = new int[7];

    public SaveFileInvaders(int[] num)
    {
        aliensDestroyed = num;
    }
}
