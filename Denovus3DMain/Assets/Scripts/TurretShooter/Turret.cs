using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    public float turnSpeed = 10f;
    private int[] gloveValue;
    private float laserTimer = 0.2f;
    [HideInInspector]
    public Transform target;
    private EnemyGolem targetEnemyGolem;
    private EnemySkeleton targetEnemySkeleton;
    public AudioSource beamAudio;
    public AudioSource projectileAudio;
    public AudioSource chargeAudio;
    public GameObject chargeGlow;
    private bool squeeze;
    private int squeezeValue;
    private bool projectilePress;
    private bool bossDoublePress;
    private readonly float refireDelay = 0.1f;

    [Header("Laser Projectile")]
    public GameObject laserPrefab;
    private bool useLaserProjectile;

    [Header("Laser Beam")]
    public int damageOverTime = 20;
    private bool useLaserBeam;
    public LineRenderer lineRenderer;
    public ParticleSystem beamImpactEffect;
    public Light impactLight;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;

    void Awake()
    {
        chargeGlow.GetComponent<ParticleSystem>().Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        squeezeValue = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;

        if (squeeze)
        {
            useLaserBeam = true;
        }
        else
        {
            useLaserProjectile = true;
        }

        InvokeRepeating(nameof(UpdateTarget), 0f, 0.2f);
        gloveValue = new int[10];
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            if (squeeze)
            {
                targetEnemyGolem = nearestEnemy.GetComponent<EnemyGolem>();
            }
            else
            {
                targetEnemySkeleton = nearestEnemy.GetComponent<EnemySkeleton>();
            }
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject progress = GameObject.Find("ProgressUI");
        ProgressTracking set = progress.GetComponent<ProgressTracking>();

        if ((set.progressBar.fillAmount == 1 && set.setNumber == 2 && target) || set.clock < 0.1f || set.nextRoundCountdown < 10f)
        {
            return;
        }
        else
        {
            if (target == null)
            {
                beamAudio.Stop();
                projectileAudio.Stop();
                bossDoublePress = false;

                if (useLaserBeam)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                        beamImpactEffect.Stop();
                        impactLight.enabled = false;
                    }
                }

                return;
            }

            LockOnTarget();

            if (useLaserBeam)
            {
                GameObject bluetoothManager = GameObject.Find("BluetoothManager");
                BTManager manager = bluetoothManager.GetComponent<BTManager>();
                for (int i = 0; i < 10; i++)
                {
                    gloveValue[i] = manager.sensorValue[i];
                }

                if (gloveValue[0] > TriggerValues.forceTrigger || gloveValue[1] > TriggerValues.forceTrigger || gloveValue[2] > TriggerValues.forceTrigger || gloveValue[3] > TriggerValues.forceTrigger || gloveValue[4] > TriggerValues.forceTrigger)
                {
                    LaserBeam();
                    laserTimer = refireDelay;
                    GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();    // Start capturing glove data
                }
                else
                {
                    laserTimer -= Time.deltaTime;

                    if (lineRenderer.enabled && laserTimer <= 0)
                    {
                        lineRenderer.enabled = false;
                        beamImpactEffect.Stop();
                        impactLight.enabled = false;
                        laserTimer = refireDelay;
                        beamAudio.Stop();
                        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();    // Stop capturing glove data
                    }
                }
            }
            else if (useLaserProjectile)
            {
                GameObject GM = GameObject.Find("GameMaster");
                WaveSpawner spawner = GM.GetComponent<WaveSpawner>();

                GameObject bluetoothManager = GameObject.Find("BluetoothManager");
                BTManager manager = bluetoothManager.GetComponent<BTManager>();
                for (int i = 0; i < 10; i++)
                {
                    gloveValue[i] = manager.sensorValue[i];
                }

                // If asking for index pinch, check for press and release
                if (PinchPopup.currentNum == 1)
                {
                    //if (gloveValue[0] > gloveTriggerValue && gloveValue[1] > gloveTriggerValue)
                    if (gloveValue[1] > TriggerValues.forceTrigger)
                    {
                        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle(); // Start capturing glove data

                        if (spawner.enemyBoss)
                        {
                            if (bossDoublePress)
                            {
                                if (!projectilePress && laserTimer <= 0f)
                                {
                                    LaserProjectile();
                                    projectilePress = true;
                                    bossDoublePress = false;
                                    chargeAudio.Stop();
                                    chargeGlow.GetComponent<ParticleSystem>().Stop();
                                }
                            }
                            else
                            {
                                if (laserTimer <= 0f)
                                {
                                    bossDoublePress = true;
                                    chargeAudio.Play();
                                    chargeGlow.GetComponent<ParticleSystem>().Play();
                                }
                            }
                            
                            laserTimer = refireDelay;
                        }
                        else
                        {
                            if (!projectilePress)
                            {
                                LaserProjectile();
                                projectilePress = true;
                            }

                            laserTimer = refireDelay;
                        }
                    }
                    else
                    {
                        laserTimer -= Time.deltaTime;

                        if (laserTimer <= 0f)
                            projectilePress = false;
                    }
                }
                // If asking for middle pinch, check for press and release
                else if (PinchPopup.currentNum == 2)
                {
                    //if (gloveValue[0] > gloveTriggerValue && gloveValue[2] > gloveTriggerValue)
                    if (gloveValue[2] > TriggerValues.forceTrigger)
                    {
                        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle(); // Start capturing glove data

                        if (spawner.enemyBoss)
                        {
                            if (bossDoublePress)
                            {
                                if (!projectilePress && laserTimer <= 0f)
                                {
                                    LaserProjectile();
                                    projectilePress = true;
                                    bossDoublePress = false;
                                    chargeAudio.Stop();
                                    chargeGlow.GetComponent<ParticleSystem>().Stop();
                                }
                            }
                            else
                            {
                                if (laserTimer <= 0f)
                                {
                                    bossDoublePress = true;
                                    chargeAudio.Play();
                                    chargeGlow.GetComponent<ParticleSystem>().Play();
                                }
                            }

                            laserTimer = refireDelay;
                        }
                        else
                        {
                            if (!projectilePress)
                            {
                                LaserProjectile();
                                projectilePress = true;
                            }

                            laserTimer = refireDelay;
                        }
                    }
                    else
                    {
                        laserTimer -= Time.deltaTime;

                        if (laserTimer <= 0f)
                            projectilePress = false;
                    }
                }
                // If asking for ring pinch, check for press and release
                else if (PinchPopup.currentNum == 3)
                {
                    //if (gloveValue[0] > gloveTriggerValue && gloveValue[3] > gloveTriggerValue)
                    if (gloveValue[3] > TriggerValues.forceTrigger)
                    {
                        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle(); // Start capturing glove data

                        if (spawner.enemyBoss)
                        {
                            if (bossDoublePress)
                            {
                                if (!projectilePress && laserTimer <= 0f)
                                {
                                    LaserProjectile();
                                    projectilePress = true;
                                    bossDoublePress = false;
                                    chargeAudio.Stop();
                                    chargeGlow.GetComponent<ParticleSystem>().Stop();
                                }
                            }
                            else
                            {
                                if (laserTimer <= 0f)
                                {
                                    bossDoublePress = true;
                                    chargeAudio.Play();
                                    chargeGlow.GetComponent<ParticleSystem>().Play();
                                }
                            }

                            laserTimer = refireDelay;
                        }
                        else
                        {
                            if (!projectilePress)
                            {
                                LaserProjectile();
                                projectilePress = true;
                            }

                            laserTimer = refireDelay;
                        }
                    }
                    else
                    {
                        laserTimer -= Time.deltaTime;

                        if (laserTimer <= 0f)
                            projectilePress = false;
                    }
                }
                // If asking for pinky pinch, check for press and release
                else if (PinchPopup.currentNum == 4)
                {
                    //if (gloveValue[0] > gloveTriggerValue && gloveValue[4] > gloveTriggerValue)
                    if (gloveValue[4] > TriggerValues.forceTrigger)
                    {
                        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle(); // Start capturing glove data

                        if (spawner.enemyBoss)
                        {
                            if (bossDoublePress)
                            {
                                if (!projectilePress && laserTimer <= 0f)
                                {
                                    LaserProjectile();
                                    projectilePress = true;
                                    bossDoublePress = false;
                                    chargeAudio.Stop();
                                    chargeGlow.GetComponent<ParticleSystem>().Stop();
                                }
                            }
                            else
                            {
                                if (laserTimer <= 0f)
                                {
                                    bossDoublePress = true;
                                    chargeAudio.Play();
                                    chargeGlow.GetComponent<ParticleSystem>().Play();
                                }
                            }

                            laserTimer = refireDelay;
                        }
                        else
                        {
                            if (!projectilePress)
                            {
                                LaserProjectile();
                                projectilePress = true;
                            }

                            laserTimer = refireDelay;
                        }
                    }
                    else
                    {
                        laserTimer -= Time.deltaTime;

                        if (laserTimer <= 0f)
                            projectilePress = false;
                    }
                }
            }
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = (target.position - transform.position) + new Vector3(0, 0.7f, 0);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void LaserBeam()
    {
        if (squeeze)
        {
            targetEnemyGolem.TakeDamage(damageOverTime * Time.deltaTime);
        }
        else
        {
            targetEnemySkeleton.TakeDamage(damageOverTime * Time.deltaTime);
        }
        

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            beamImpactEffect.Play();
            impactLight.enabled = true;
            beamAudio.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position + new Vector3(0, 0.7f, 0));

        Vector3 dir = (firePoint.position - target.position) + new Vector3(30, 60, 0);

        beamImpactEffect.transform.position = target.position + dir.normalized;

        beamImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void LaserProjectile()
    {
        GameObject laserGO = (GameObject)Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Laser laser = laserGO.GetComponent<Laser>();

        projectileAudio.Play();

        if (laser != null)
        {
            laser.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void ResetHealth()
    {
        if (squeeze)
            targetEnemyGolem.ResetHealth();
        else
            return;
    }
}
