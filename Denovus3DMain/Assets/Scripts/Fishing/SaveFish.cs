using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class SaveFish : MonoBehaviour
{
    public void SaveFishValues(float newFish) //use for save
    {
        SaveFileFish fishFile = new SaveFileFish(new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f });
        if (File.Exists(Application.persistentDataPath + "/savefilefish.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefilefish.save", FileMode.Open);
            SaveFileFish save = (SaveFileFish)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            fishFile = save;
        }
        for (int i = 0; i < 7; i++)
        {
            fishFile.fishTime[i] = fishFile.fishTime[i + 1];
        }
        fishFile.fishTime[6] = newFish;
        BinaryFormatter binF = new BinaryFormatter();
        FileStream fileNew = File.Create(Application.persistentDataPath + "/savefilefish.save");
        binF.Serialize(fileNew, fishFile);
        fileNew.Close();
    }

    public void LoadFishValues() //use for startup
    {
        if (File.Exists(Application.persistentDataPath + "/savefilefish.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefilefish.save", FileMode.Open);
            SaveFileFish save = (SaveFileFish)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
        }
    }
}
