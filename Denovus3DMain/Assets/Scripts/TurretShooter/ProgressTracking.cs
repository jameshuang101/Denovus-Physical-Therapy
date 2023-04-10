using UnityEngine;
using UnityEngine.UI;

public class ProgressTracking : MonoBehaviour
{
    [Header("General")]
    private float killGoal;
    private float killTotal = 0f;
    private int previousDeathCount = 1;
    private int deathCount = 0;
    private int bossCount = 0;
    public float clock;
    [HideInInspector]
    public int setNumber = 1;
    [HideInInspector]
    public float nextRoundCountdown = 10f;
    private bool isSqueezed;
    private bool retryShow;
    private float retrySqueezeDelay = 2f;
    private float finalCountdown = 6f;
    private bool finalCheck;
    private bool squeeze;
    private int squeezeValue;
    private readonly float addedTimeAmount = 15f;
    [HideInInspector]
    public float totalTime = 0f;
    private bool bossTextShown;
    private int totalScore = 0;
    private bool showValues;
    [HideInInspector]
    public bool exerciseComplete;

    [Header("Unity Setup")]
    public Image progressBar;
    public Text countdownText;
    public Text setNumberText;
    public GameObject AddedTimeTextMesh;
    public Text nextRoundCountdownText;
    public Text finalRoundText;
    public Text outOfTimeText;
    public Text outOfTimePinchText;
    public Text goodJobText;
    public Text goodJobTextFinal1;
    public Text goodJobTextFinal2;
    public Text bossText;
    public Image golemIcon;
    public Image skellyIcon;
    public Text setTextOf2;
    public Text scoreText;
    public Image skellyScore;

    void Awake()
    {
        bossText.transform.Translate(0f, -500f, 0f);
        //used for testing
        //PlayerPrefs.SetInt("Squeeze", 1);
        //PlayerPrefs.SetInt("Difficulty", 1);
        //PlayerPrefs.SetString("Scene", "Turret");
    }

    void Start()
    {
        squeezeValue = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;

        setNumberText.text = setNumber.ToString();
        nextRoundCountdownText.text = Mathf.Floor(nextRoundCountdown).ToString();
        progressBar.fillAmount = 0;

        if (squeeze)
        {
            skellyIcon.transform.Translate(0f, -500f, 0f);
            skellyScore.transform.Translate(0f, -500f, 0f);
            clock = 60f;
            killGoal = 10f;

        }
        else
        {
            golemIcon.transform.Translate(0f, -500f, 0f);
            killGoal = 4f;
            setTextOf2.transform.Translate(0f, -500f, 0f);

            // Difficulty scalar
            int difficulty = PlayerPrefs.GetInt("Difficulty", 1);
            if (difficulty == 1)
                clock = 150f;
            else if (difficulty == 2)
                clock = 180f;
            else
                clock = 210f;

        }
    }

    void Update()
    {
        //// Used for testing runtime sensor lb readings
        //GameObject bluetoothManager = GameObject.Find("BluetoothManager");
        //BTManager manager = bluetoothManager.GetComponent<BTManager>();
        //float poundValue;

        //if (manager.sensorValue[2] >= 0f && manager.sensorValue[2] <= 300f)
        //    poundValue = (.0083f * manager.sensorValue[2])/1.7f;
        //else if (manager.sensorValue[2] >= 300f && manager.sensorValue[2] <= 440f)
        //    poundValue = ((.0179f * manager.sensorValue[2]) - 2.8571f)/1.7f;
        //else if (manager.sensorValue[2] >= 440f && manager.sensorValue[2] <= 550f)
        //    poundValue = ((.0227f * manager.sensorValue[2]) - 5f)/1.7f;
        //else if (manager.sensorValue[2] >= 550f && manager.sensorValue[2] <= 730f)
        //    poundValue = ((.0139f * manager.sensorValue[2]) - .1389f)/1.7f;
        //else
        //    poundValue = 0f;

        //Debug.Log(poundValue + "lbs");

        // If user is out of time before filling progress bar
        if (clock <= 0.1f)
        {
            // If golem, stop all laser audio and effects
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (squeeze)
            {
                EnemyGolem e = enemy.GetComponent<EnemyGolem>();

                GameObject turret = GameObject.Find("LaserGun");
                Turret t = turret.GetComponent<Turret>();

                if (t.lineRenderer.enabled)
                {
                    t.lineRenderer.enabled = false;
                    t.beamImpactEffect.Stop();
                    t.impactLight.enabled = false;
                }

                t.beamAudio.Stop();
                e.hitAudio.Stop();

                if (!retryShow)
                {
                    outOfTimeText.transform.Translate(0f, 5f, 0f);
                    retryShow = true;
                    FindObjectOfType<AudioManager>().Play("Fail");
                }

                BTManager manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

                if (retrySqueezeDelay >= 0f)
                {
                    retrySqueezeDelay -= Time.deltaTime;
                }

                else if (manager.sensorValue[0] > TriggerValues.forceTrigger || 
                         manager.sensorValue[1] > TriggerValues.forceTrigger || 
                         manager.sensorValue[2] > TriggerValues.forceTrigger || 
                         manager.sensorValue[3] > TriggerValues.forceTrigger || 
                         manager.sensorValue[4] > TriggerValues.forceTrigger)
                {
                    isSqueezed = true;
                }

                else if (isSqueezed)
                {
                    if (nextRoundCountdown >= 0.1f)
                    {
                        if (nextRoundCountdown == 10f)
                        {
                            outOfTimeText.transform.Translate(0f, -5f, 0f);
                            finalRoundText.transform.Translate(0f, 5f, 0f);
                        }

                        nextRoundCountdown -= Time.deltaTime;
                        nextRoundCountdownText.text = Mathf.Floor(nextRoundCountdown).ToString();
                    }
                    else
                    {
                        GameObject turret2 = GameObject.Find("LaserGun");
                        Turret t2 = turret2.GetComponent<Turret>();
                        t2.ResetHealth();

                        finalRoundText.transform.Translate(0f, -5f, 0f);
                        killTotal = 0f;
                        progressBar.fillAmount = killTotal / killGoal;
                        nextRoundCountdown = 10f;
                        clock = 60f;
                        retryShow = false;
                        isSqueezed = false;
                        retrySqueezeDelay = 2f;
                    }
                }
            }
            // Else skelly, stop all audio and effects
            else
            {
                exerciseComplete = true;

                EnemySkeleton e = enemy.GetComponent<EnemySkeleton>();

                GameObject turret = GameObject.Find("LaserGun");
                Turret t = turret.GetComponent<Turret>();

                if (t.lineRenderer.enabled)
                {
                    t.lineRenderer.enabled = false;
                    t.beamImpactEffect.Stop();
                    t.impactLight.enabled = false;
                }

                if (!finalCheck)
                {
                    GameObject.Find("FinalGloveValuesUI").GetComponent<FinalGloveValues>().UpdateValues();
                    finalCheck = true;
                }

                t.beamAudio.Stop();
                e.hitAudio.Stop();

                if (!retryShow)
                {
                    outOfTimePinchText.transform.Translate(0f, 5f, 0f);
                    retryShow = true;
                    FindObjectOfType<AudioManager>().Play("Victory");
                }

                BTManager manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

                if (retrySqueezeDelay >= 0f)
                {
                    retrySqueezeDelay -= Time.deltaTime;
                }

                else if (manager.sensorValue[0] > TriggerValues.forceTrigger || 
                         manager.sensorValue[1] > TriggerValues.forceTrigger || 
                         manager.sensorValue[2] > TriggerValues.forceTrigger || 
                         manager.sensorValue[3] > TriggerValues.forceTrigger || 
                         manager.sensorValue[4] > TriggerValues.forceTrigger)
                {
                    golemIcon.transform.Translate(0f, 500f, 0f);
                    setTextOf2.transform.Translate(0f, 500f, 0f);
                    PlayerPrefs.SetInt("SkellyScore", totalScore);
                    GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().SwitchExerciseToMain();
                }
            }

            // Show appropriate fail message

            clock += Time.deltaTime;
        }

        // Successful set loop
        else if (progressBar.fillAmount == 1)
        {
            // If user completed set 1
            if (squeeze)
            {
                if (setNumber == 1)
                {
                    // Reset timer if below 60
                    if (clock < 60f)
                    {
                        clock = 60f;
                    }
                    if (nextRoundCountdown >= 0.1f)
                    {
                        if (nextRoundCountdown == 10f)
                        {
                            FindObjectOfType<AudioManager>().Play("Victory");
                            goodJobText.transform.Translate(0f, 5f, 0f);
                            finalRoundText.transform.Translate(0f, 5f, 0f);
                        }

                        clock += Time.deltaTime;
                        nextRoundCountdown -= Time.deltaTime;
                        nextRoundCountdownText.text = Mathf.Floor(nextRoundCountdown).ToString();
                    }
                    else
                    {
                        goodJobText.transform.Translate(0f, -5f, 0f);
                        finalRoundText.transform.Translate(0f, -5f, 0f);
                        setNumber = 2;
                        setNumberText.text = setNumber.ToString();
                        killTotal = 0f;
                        progressBar.fillAmount = killTotal / killGoal;
                        nextRoundCountdown = 10f;
                    }

                }
                // Else user has completed both exercise sets
                else
                {
                    exerciseComplete = true;

                    if (!showValues)
                    {
                        GameObject.Find("FinalGloveValuesUI").GetComponent<FinalGloveValues>().UpdateValues();
                        showValues = true;
                    }

                    clock += Time.deltaTime;

                    if (finalCountdown >= 0f)
                    {
                        if (finalCountdown == 6f)
                        {
                            FindObjectOfType<AudioManager>().Play("Victory");
                            goodJobTextFinal1.transform.Translate(0f, 5f, 0f);
                        }

                        finalCountdown -= Time.deltaTime;

                    }
                    else
                    {
                        if (!finalCheck)
                        {
                            goodJobTextFinal1.transform.Translate(0f, -5f, 0f);
                            goodJobTextFinal2.transform.Translate(0f, 5f, 0f);
                            finalCheck = true;
                        }

                        BTManager manager = GameObject.Find("BluetoothManager").GetComponent<BTManager>();

                        if (retrySqueezeDelay >= 0f)
                        {
                            retrySqueezeDelay -= Time.deltaTime;
                        }
                        else if (manager.sensorValue[0] > TriggerValues.forceTrigger ||
                                 manager.sensorValue[1] > TriggerValues.forceTrigger ||
                                 manager.sensorValue[2] > TriggerValues.forceTrigger ||
                                 manager.sensorValue[3] > TriggerValues.forceTrigger ||
                                 manager.sensorValue[4] > TriggerValues.forceTrigger)
                        {
                            //GetComponent<SaveStone>().SaveStoneValues(totalTime);
                            skellyIcon.transform.Translate(0f, 500f, 0f);
                            skellyScore.transform.Translate(0f, 500f, 0f);

                            GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().SwitchExerciseToMain();
                        }
                    }
                }
            }

            // Else progress bar full and skelly
            else
            {
                if (nextRoundCountdown >= 0.1f)
                {
                    if (nextRoundCountdown == 10f)
                    {
                        FindObjectOfType<AudioManager>().Play("Victory");
                        goodJobText.transform.Translate(0f, 5f, 0f);
                        finalRoundText.transform.Translate(0f, 5f, 0f);
                    }

                    clock += Time.deltaTime;
                    nextRoundCountdown -= Time.deltaTime;
                    nextRoundCountdownText.text = Mathf.Floor(nextRoundCountdown).ToString();
                }
                else
                {
                    goodJobText.transform.Translate(0f, -5f, 0f);
                    finalRoundText.transform.Translate(0f, -5f, 0f);
                    setNumber++;
                    setNumberText.text = setNumber.ToString();
                    killTotal = 0f;
                    progressBar.fillAmount = killTotal / killGoal;
                    nextRoundCountdown = 10f;
                }
            }
        }

        clock -= Time.deltaTime;
        countdownText.text = Mathf.Floor(clock).ToString();

        GameObject GM = GameObject.Find("GameMaster");
        WaveSpawner spawner = GM.GetComponent<WaveSpawner>();
        deathCount = spawner.deathCount;
        bossCount = spawner.bossCount;
        totalScore = (deathCount - 1) + (bossCount * 2);
        if (totalScore < 0)
            scoreText.text = 0.ToString();
        else
            scoreText.text = totalScore.ToString();

        totalTime += Time.deltaTime;

        // Display added time when enemy killed
        if (deathCount > previousDeathCount)
        {
            if (squeeze)
            {
                Destroy(Instantiate(AddedTimeTextMesh, new Vector3(-1.56f, 6.49f, -10.25f), transform.rotation), 1.45f);
                clock += addedTimeAmount;
            }

            killTotal++;
            progressBar.fillAmount = killTotal / killGoal;
            previousDeathCount = deathCount;
        }

        if (spawner.enemyBoss)
        {
            if (!bossTextShown)
            {
                bossText.transform.Translate(0f, 500f, 0f);
                bossTextShown = true;
            }
        }
        else
        {
            if (bossTextShown)
            {
                bossText.transform.Translate(0f, -500f, 0f);
                bossTextShown = false;
            }
        }
    }
}
