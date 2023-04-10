using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;
using UnityEngine.UI;

public class PopulateProgress : MonoBehaviour
{
    int[] space = new int[7];
    int[] skelly = new int[7];
    float[] golem = new float[7];
    float[] fish = new float[7];
    public Text golemText, skellyText, fishText, spaceText;

    // Start is called before the first frame update
    void Start()
    {
        space = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        skelly = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        golem = new float[] { 0, 0, 0, 0, 0, 0, 0 };
        fish = new float[] { 0, 0, 0, 0, 0, 0, 0 };
        UpdateProgress();
    }

    public void UpdateProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/savefilebone.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/savefilebone.save", FileMode.Open);
            SaveFileBone saveSkelly = (SaveFileBone)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            skelly = saveSkelly.enemiesKilled;
        }
        if (File.Exists(Application.persistentDataPath + "/savefilestone.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/savefilestone.save", FileMode.Open);
            SaveFileStone saveGolem = (SaveFileStone)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            golem = saveGolem.time;
        }
        if (File.Exists(Application.persistentDataPath + "/savefilefish.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/savefilefish.save", FileMode.Open);
            SaveFileFish saveFish = (SaveFileFish)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            fish = saveFish.fishTime;
        }
        if (File.Exists(Application.persistentDataPath + "/savefileinvaders.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/savefileinvaders.save", FileMode.Open);
            SaveFileInvaders saveInvader = (SaveFileInvaders)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            space = saveInvader.aliensDestroyed;
        }

        golemText.text = golem[6].ToString();
        fishText.text = fish[6].ToString();
        skellyText.text = skelly[6].ToString();
        spaceText.text = space[6].ToString();
    }
}
