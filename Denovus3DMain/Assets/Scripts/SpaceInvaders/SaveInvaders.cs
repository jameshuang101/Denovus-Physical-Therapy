using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class SaveInvaders : MonoBehaviour
{
    public void SaveInvaderValues(int totalAliens)
    {
        SaveFileInvaders invaderFile = new SaveFileInvaders(new int[] { 0, 0, 0, 0, 0, 0, 0 });
        if (File.Exists(Application.persistentDataPath + "/savefileinvaders.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefileinvaders.save", FileMode.Open);
            SaveFileInvaders save = (SaveFileInvaders)bf.Deserialize(file); //use save.xyz to access variables
            file.Close();
            invaderFile = save;
        }
        for (int i = 0; i < 7; i++)
        {
            invaderFile.aliensDestroyed[i] = invaderFile.aliensDestroyed[i + 1];
        }
        invaderFile.aliensDestroyed[6] = totalAliens;
        BinaryFormatter binF = new BinaryFormatter();
        FileStream fileNew = File.Create(Application.persistentDataPath + "/savefileinvaders.save");
        binF.Serialize(fileNew, invaderFile);
        fileNew.Close();
    }

    public void LoadInvaderValues()
    {
        if (File.Exists(Application.persistentDataPath + "/savefileinvaders.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefileinvaders.save", FileMode.Open);
            SaveFileInvaders save = (SaveFileInvaders)bf.Deserialize(file);
            file.Close();
        }
    }
}
