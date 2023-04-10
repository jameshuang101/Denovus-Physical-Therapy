using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class SaveStone : MonoBehaviour
{
    public void SaveStoneValues(float newTime) //use for save
    {
        SaveFileStone stoneFile = new SaveFileStone(new float[]{0f,0f,0f,0f,0f,0f,0f});
        if (File.Exists(Application.persistentDataPath + "/savefilestone.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefilestone.save", FileMode.Open);
            SaveFileStone save = (SaveFileStone)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            stoneFile = save;
        }
        for(int i = 0; i < 7; i++)
        {
            stoneFile.time[i] = stoneFile.time[i + 1];
        }
        stoneFile.time[6] = newTime;
        BinaryFormatter binF = new BinaryFormatter();
        FileStream fileNew = File.Create(Application.persistentDataPath + "/savefilestone.save");
        binF.Serialize(fileNew, stoneFile);
        fileNew.Close();
    }

    public void LoadStoneValues() //use for startup (prob wont need)
    {
        if (File.Exists(Application.persistentDataPath + "/savefilestone.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefilestone.save", FileMode.Open);
            SaveFileStone save = (SaveFileStone)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
        }
    }
}
