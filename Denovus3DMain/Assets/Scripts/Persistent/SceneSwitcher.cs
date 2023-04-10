using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject[] DontDestroyObjectsList;
    [HideInInspector]
    public bool squeeze = false;
    [HideInInspector]
    public int difficulty;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        foreach (GameObject g in DontDestroyObjectsList)
            DontDestroyOnLoad(g);
        GameObject.Find("StarterPlane").SetActive(true);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = false;
        difficulty = 1;
    }

    public void SwitchCallibrationToMain()
    {
        SceneManager.LoadScene("MainMenu");
        GameObject.Find("StarterPlane").SetActive(true);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = false;
    }

    public void SwitchMainToCallibration()
    {
        SceneManager.LoadScene("Calibration");
        GameObject.Find("StarterPlane").SetActive(true);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = false;
        GameObject.Find("CalibrationPanelText").GetComponent<Text>().text = "Hi, welcome to Denovus VR Stroke Therapy. To calibrate your table, please follow the instructions on screen.\nClick to continue ->";
    }

    public void SwitchMainToExercise1() //Space invaders
    {
        difficulty = (int) GameObject.Find("TaskListPanel/Slider").gameObject.GetComponent<Slider>().value;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetString("Scene", "Space");
        SceneManager.LoadScene("TutorialInvaders");
        GameObject.Find("StarterPlane").SetActive(false);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = true;
    }

    public void SwitchMainToExercise2() //Stone shooter
    {
        difficulty = (int)GameObject.Find("TaskListPanel/Slider").gameObject.GetComponent<Slider>().value;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("Squeeze", 1);
        PlayerPrefs.SetString("Scene", "Turret");
        squeeze = true;
        SceneManager.LoadScene("DenovusTurretShooterIntroduction");
        GameObject.Find("StarterPlane").SetActive(false);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = true;
    }

    public void SwitchMainToExercise3() //Fishing
    {
        difficulty = (int)GameObject.Find("TaskListPanel/Slider").gameObject.GetComponent<Slider>().value;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetString("Scene", "Fish");
        SceneManager.LoadScene("TutorialFishing");
        GameObject.Find("StarterPlane").SetActive(false);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = true;
    }

    public void SwitchMainToExercise4() //Bone shooter
    {
        difficulty = (int)GameObject.Find("TaskListPanel/Slider").gameObject.GetComponent<Slider>().value;
        PlayerPrefs.SetInt("Difficulty",    difficulty);
        PlayerPrefs.SetInt("Squeeze", 2);
        PlayerPrefs.SetString("Scene", "Turret");
        squeeze = false;
        SceneManager.LoadScene("DenovusTurretShooterIntroduction");
        GameObject.Find("StarterPlane").SetActive(false);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = true;
    }

    public void SwitchExerciseToMain()
    {
        SceneManager.LoadScene("MainMenu");
        GameObject.Find("StarterPlane").SetActive(true);
        GameObject.Find("SaveManager").GetComponent<SaveSensors>().enabled = false;
        GameObject.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetInt("Difficulty", 1);
    }
}
