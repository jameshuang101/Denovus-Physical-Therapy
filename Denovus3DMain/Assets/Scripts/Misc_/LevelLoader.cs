using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public Text message1;
    public Text message2;
    public Text message3;
    public Text message4;

    private float count = 0f;

    void Start ()
    {
        int num = Random.Range(1, 4);

        if (num == 1)
            message1.transform.Translate(0f, 4.26f, 0f);
        else if (num == 2)
            message2.transform.Translate(0f, 4.26f, 0f);
        else if (num == 3)
            message3.transform.Translate(0f, 4.26f, 0f);
        else if (num == 4)
            message4.transform.Translate(0f, 4.26f, 0f);
    }

    void Update()
    {
        count += Time.deltaTime;

        if (count < 5f)
            slider.value = count / 5f;
        else
            LoadLevel();
    }

    public void LoadLevel()
    {
        if (PlayerPrefs.GetString("Scene") == "Turret")
            SceneManager.LoadScene("DenovusTurretShooter");
        else if (PlayerPrefs.GetString("Scene") == "Space")
            SceneManager.LoadScene("DenovusSpaceInvaders");
        else if (PlayerPrefs.GetString("Scene") == "Fish")
            SceneManager.LoadScene("DenovusFishing");
    }
}
