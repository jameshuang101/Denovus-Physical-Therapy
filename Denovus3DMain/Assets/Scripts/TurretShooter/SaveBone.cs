using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class SaveBone : MonoBehaviour
{
    public void SaveBoneValues(int newEnemiesKilled) //use for save
    {
        SaveFileBone boneFile = new SaveFileBone(new int[] { 0, 0, 0, 0, 0, 0, 0 });
        if (File.Exists(Application.persistentDataPath + "/savefilebone.save")) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefilebone.save", FileMode.Open);
            SaveFileBone save = (SaveFileBone)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            boneFile = save;
        }
        for (int i = 0; i < 7; i++)
        {
            boneFile.enemiesKilled[i] = boneFile.enemiesKilled[i + 1];
        }
        boneFile.enemiesKilled[6] = newEnemiesKilled;
        BinaryFormatter binF = new BinaryFormatter();
        FileStream fileNew = File.Create(Application.persistentDataPath + "/savefilebone.save");
        binF.Serialize(fileNew, boneFile);
        fileNew.Close();
    }

    public void LoadBoneValues() //use for startup
    {
        if (File.Exists(Application.persistentDataPath + "/savefilebone.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefilebone.save", FileMode.Open);
            SaveFileBone save = (SaveFileBone)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
        }
    }
}
