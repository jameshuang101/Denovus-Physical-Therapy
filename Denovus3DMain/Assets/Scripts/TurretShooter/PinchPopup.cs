using UnityEngine;
using UnityEngine.UI;

public class PinchPopup : MonoBehaviour
{
    public Image indexPinch;
    public Image middlePinch;
    public Image ringPinch;
    public Image pinkyPinch;
    public Text x2Text;
    public Image squeezeImage;
    private ProgressTracking set;

    private bool squeeze;
    private int squeezeValue;
    [HideInInspector]
    public int[] limbs;
    [HideInInspector]
    public int counterCheck = 0;
    private int lastNum = 0;
    [HideInInspector]
    public static int currentNum = 0;
    private bool x2Shown;
    private bool squeezeShown;

    // Start is called before the first frame update
    void Awake()
    {
        limbs = new int[4];

        indexPinch.transform.Translate(0f, -5f, 0f);
        middlePinch.transform.Translate(0f, -5f, 0f);
        ringPinch.transform.Translate(0f, -5f, 0f);
        pinkyPinch.transform.Translate(0f, -5f, 0f);
        x2Text.transform.Translate(0f, -5f, 0f);
        squeezeImage.transform.Translate(0f, -5f, 0f);

        InvokeRepeating(nameof(UpdateEnemyValues), 3f, 0.5f);
    }

    void Start()
    {
        //squeeze = GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().squeeze;

        squeezeValue = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;
    }

    void UpdateEnemyValues()
    {
        set = GameObject.Find("ProgressUI").GetComponent<ProgressTracking>();

        if (set.exerciseComplete)
            Destroy(gameObject);

        if (squeeze)
        {
            GameObject laser = GameObject.Find("LaserGun");
            Turret turret = laser.GetComponent<Turret>();

            if (turret.target)
            {
                if (!squeezeShown)
                {
                    squeezeImage.transform.Translate(0f, 5f, 0f);
                    squeezeShown = true;
                }
            }
            else
            {
                if (squeezeShown)
                {
                    squeezeImage.transform.Translate(0f, -5f, 0f);
                    squeezeShown = false;
                }
            }
        }
        else
        {
            // If time up move all icons down
            if (set.clock < 0.1f)
            {
                MoveLogo();
            }

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            EnemySkeleton skelly = enemy.GetComponent<EnemySkeleton>();
            EnemySkeletonMovement skellyMove = enemy.GetComponent<EnemySkeletonMovement>();

            for (int i = 0; i < skelly.limbValues.Length; i++)
            {
                limbs[i] = skelly.limbValues[i];
            }

            // Clear icons until skelly has stopped moving
            if (skellyMove.move)
            {
                if (counterCheck == 4)
                {
                    if (lastNum == 1)
                    {
                        indexPinch.transform.Translate(0f, -5f, 0f);
                    }
                    else if (lastNum == 2)
                    {
                        middlePinch.transform.Translate(0f, -5f, 0f);
                    }
                    else if (lastNum == 3)
                    {
                        ringPinch.transform.Translate(0f, -5f, 0f);
                    }
                    else if (lastNum == 4)
                    {
                        pinkyPinch.transform.Translate(0f, -5f, 0f);
                    }

                    counterCheck = 0;
                }
            }
            else
            {
                if ((counterCheck == 0 && skelly.counter == 0) || (counterCheck == 1 && skelly.counter == 1) || (counterCheck == 2 && skelly.counter == 2) || (counterCheck == 3 && skelly.counter == 3))
                {
                    MoveLogo();
                }
            }
        }
    }

    public void MoveLogo()
    {
        lastNum = limbs[3];

        currentNum = limbs[counterCheck];

        GameObject progress = GameObject.Find("ProgressUI");
        ProgressTracking set = progress.GetComponent<ProgressTracking>();

        GameObject GM = GameObject.Find("GameMaster");
        WaveSpawner spawner = GM.GetComponent<WaveSpawner>();

        if (spawner.enemyBoss)
        {
            if (!x2Shown)
            {
                x2Text.transform.Translate(0f, 5f, 0f);
                x2Shown = true;
            }
        }
        else
        {
            if (x2Shown)
            {
                x2Text.transform.Translate(0f, -5f, 0f);
                x2Shown = false;
            }
        }

        // Move other icons back down, if needed
        if (counterCheck > 0)
        {
            if (limbs[counterCheck - 1] == 1)
            {
                indexPinch.transform.Translate(0f, -5f, 0f);
            }
            else if (limbs[counterCheck - 1] == 2)
            {
                middlePinch.transform.Translate(0f, -5f, 0f);
            }
            else if (limbs[counterCheck - 1] == 3)
            {
                ringPinch.transform.Translate(0f, -5f, 0f);
            }
            else if (limbs[counterCheck - 1] == 4)
            {
                pinkyPinch.transform.Translate(0f, -5f, 0f);
            }
        }

        // Move icons up
        if (set.clock > 0.1f)
        {
            if (limbs[counterCheck] == 1)
            {
                indexPinch.transform.Translate(0f, 5f, 0f);
            }
            else if (limbs[counterCheck] == 2)
            {
                middlePinch.transform.Translate(0f, 5f, 0f);
            }
            else if (limbs[counterCheck] == 3)
            {
                ringPinch.transform.Translate(0f, 5f, 0f);
            }
            else if (limbs[counterCheck] == 4)
            {
                pinkyPinch.transform.Translate(0f, 5f, 0f);
            }
        }

        counterCheck++;
    }
}
