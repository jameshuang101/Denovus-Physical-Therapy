using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float xScale = PlayerPrefs.GetFloat("TableXScale", 1f);
        float yScale = PlayerPrefs.GetFloat("TableYScale", 1f);
        float zScale = PlayerPrefs.GetFloat("TableZScale", 1f);
        this.transform.localScale = new Vector3(xScale, yScale, zScale);
    }  
}
