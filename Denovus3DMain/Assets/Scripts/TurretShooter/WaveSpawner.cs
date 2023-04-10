using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private Transform target;
    public Transform enemyGolemPrefab;
    public Transform enemySkeletonPrefab;
    public Transform enemySkeletonBossPrefab;
    public Transform spawnPoint;
    private float respawnDelay = 2f;
    [HideInInspector]
    public int deathCount = 0;
    private bool squeeze;               
    private int squeezeValue;
    [HideInInspector]
    public bool enemyBoss;
    [HideInInspector]
    public int bossCount = 0;

    void Start()
    {
        squeezeValue = PlayerPrefs.GetInt("Squeeze", 2);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;
    }

    void Update()
    {
        GameObject progress = GameObject.Find("ProgressUI");
        ProgressTracking set = progress.GetComponent<ProgressTracking>();

        if (respawnDelay > 0f)
        {
            respawnDelay -= Time.deltaTime;
            if (respawnDelay <= 0f)
            {
                SpawnEnemy();
            }
        }
        else
        {
            SpawnEnemy();
        }

        GameObject enem = GameObject.FindGameObjectWithTag("Enemy");
        Animator anim = enem.GetComponent<Animator>();

        GameObject laserGun = GameObject.Find("LaserGun");
        Turret turret = laserGun.GetComponent<Turret>();
        target = turret.target;

        if (set.progressBar.fillAmount == 1 && set.setNumber == 2 && target && set.clock > 0.1f && squeeze)
            anim.SetBool("Die", true);
        else if (set.clock < 0.1f && !squeeze)
            anim.SetBool("Die", true);
    }

    void SpawnEnemy()
    {
        if (target != null)
        {
            return;
        }
        else
        {
            if (squeeze)
            {
                Instantiate(enemyGolemPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                if ((deathCount >= 8 && deathCount <= 11)
                    || (deathCount >= 20 && deathCount <= 23)
                    || (deathCount >= 32 && deathCount <= 35)
                    || (deathCount >= 44 && deathCount <= 47))
                {
                    enemyBoss = true;
                    Instantiate(enemySkeletonBossPrefab, spawnPoint.position, spawnPoint.rotation);
                    if (deathCount > 8 || deathCount > 20 || deathCount > 32 || deathCount > 44)
                        bossCount++;
                }
                else
                {
                    enemyBoss = false;
                    Instantiate(enemySkeletonPrefab, spawnPoint.position, spawnPoint.rotation);
                    if (deathCount == 12 || deathCount == 24 || deathCount == 36 || deathCount == 48)
                        bossCount++;
                }
            }
            
            respawnDelay = 6f;
            deathCount++;
        }
    }
}
