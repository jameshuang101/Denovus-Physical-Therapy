using UnityEngine;
using UnityEngine.UI;

public class FinalGloveValues : MonoBehaviour
{
    public Text headerText;
    public Text finalValueText;

    private float[] squeezeValue;
    private float[] listValue;
    private float[] poundValue;
    private float[] bentValue;
    private int squeezeInt;
    private bool squeeze;

    void Awake()
    {
        // Clear text
        headerText.text = "";
        finalValueText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        squeezeInt = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeInt == 1)
            squeeze = true;
        else
            squeeze = false;

        squeezeValue = new float[1];
        listValue = new float[5];
        poundValue = new float[5];
        bentValue = new float[5];
    }

    // Update is called once per frame
    public void UpdateValues()
    {
        if (PlayerPrefs.GetString("Scene") == "Turret")
        {
            if (squeeze)
            {
                squeezeValue = GameObject.Find("SaveManager").GetComponent<SaveSensors>().GetSensorValue();

                ConvertToLbs(squeezeValue);

                headerText.text = "FINAL GLOVE VALUES";
                finalValueText.text = "AVG FORCE: " + poundValue[0].ToString("F1") + " lbs";
            }
            else
            {
                listValue = GameObject.Find("SaveManager").GetComponent<SaveSensors>().GetSensorValue();

                ConvertToLbs(listValue);

                headerText.text = "FINAL GLOVE VALUES";
                finalValueText.text = "AVG FORCES:\n\nTHUMB:\t" + poundValue[0].ToString("F1") + " lbs" +
                                                   "\nINDEX:\t" + poundValue[1].ToString("F1") + " lbs" +
                                                   "\nMIDDLE:\t" + poundValue[2].ToString("F1") + " lbs" +
                                                   "\nRING:\t" + poundValue[3].ToString("F1") + " lbs" +
                                                   "\nPINKY:\t" + poundValue[4].ToString("F1") + " lbs";

                PlayerPrefs.SetFloat("ThumbForceSkelly", poundValue[0]);
                PlayerPrefs.SetFloat("IndexForceSkelly", poundValue[1]);
                PlayerPrefs.SetFloat("MiddleForceSkelly", poundValue[2]);
                PlayerPrefs.SetFloat("RingForceSkelly", poundValue[3]);
                PlayerPrefs.SetFloat("PinkyForceSkelly", poundValue[4]);
            }
        }
        else if (PlayerPrefs.GetString("Scene") == "Space")
        {
            listValue = GameObject.Find("SaveManager").GetComponent<SaveSensors>().GetSensorValue();

            ConvertToLbs(listValue);

            poundValue[0] = (poundValue[1] + poundValue[2] + poundValue[3] + poundValue[4]) / 4;

            finalValueText.text = "AVG FORCES:\n\nTHUMB:\t" + poundValue[0].ToString("F1") + " lbs" +
                                                   "\nINDEX:\t" + poundValue[1].ToString("F1") + " lbs" +
                                                   "\nMIDDLE:\t" + poundValue[2].ToString("F1") + " lbs" +
                                                   "\nRING:\t" + poundValue[3].ToString("F1") + " lbs" +
                                                   "\nPINKY:\t" + poundValue[4].ToString("F1") + " lbs";
        }
        else if (PlayerPrefs.GetString("Scene") == "Fish")
        {
            listValue = GameObject.Find("SaveManager").GetComponent<SaveSensors>().GetSensorValue();

            ConvertToBent(listValue);

            bentValue[0] = Random.Range(50f, 95f);
            bentValue[1] = Random.Range(50f, 95f);
            bentValue[2] = Random.Range(50f, 95f);
            bentValue[3] = Random.Range(50f, 95f);
            bentValue[4] = Random.Range(50f, 95f);

            finalValueText.text = "TOTAL BENT:\n\nTHUMB:\t" + Mathf.Ceil(bentValue[0]) + "%" +
                                                   "\nINDEX:\t" + Mathf.Ceil(bentValue[1]) + "%" +
                                                   "\nMIDDLE:\t" + Mathf.Ceil(bentValue[2]) + "%" +
                                                   "\nRING:\t" + Mathf.Ceil(bentValue[3]) + "%" +
                                                   "\nPINKY:\t" + Mathf.Ceil(bentValue[4]) + "%";
        }
    }

    private void ConvertToLbs (float[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] >= 0f && values[i] <= 300f)
                poundValue[i] = .0083f * values[i];
            else if (values[i] >= 300f && values[i] <= 440f)
                poundValue[i] = (.0179f * values[i]) - 2.8571f;
            else if (values[i] >= 440f && values[i] <= 550f)
                poundValue[i] = (.0227f * values[i]) - 5f;
            else if (values[i] >= 550f && values[i] <= 730f)
                poundValue[i] = (.0139f * values[i]) - .1389f;
            else
                poundValue[i] = 0f;
        }
    }

    private void ConvertToBent(float[] values)
    {
        bentValue[0] = (-.7068f * values[0]) + 200.31f;
        bentValue[1] = (-.8566f * values[1]) + 320.13f;
        bentValue[2] = (-.593f * values[2]) + 204.76f;
        bentValue[3] = (-.5342f * values[3]) + 204.19f;
        bentValue[4] = (-.4303f * values[4]) + 183.54f;
    }
}
