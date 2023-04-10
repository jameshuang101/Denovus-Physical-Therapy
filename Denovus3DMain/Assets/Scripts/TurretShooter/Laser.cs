using UnityEngine;

public class Laser : MonoBehaviour
{
    private Transform target;

    [Header("General")]
    public float speed = 7f;
    private readonly int damage = 50;
    private bool squeeze;
    private int squeezeValue;

    [Header("Unity Setup")]
    public GameObject impactEffect;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        //squeeze = GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().squeeze;

        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle(); // Stop capturing glove data

        squeezeValue = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position) + new Vector3(0, 0.55f, 0);
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget()
    {
        GameObject effecIns =  (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effecIns, 1f);

        Damage(target);

        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        if (squeeze)
        {
            EnemyGolem e = enemy.GetComponent<EnemyGolem>();

            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
        else
        {
            EnemySkeleton e = enemy.GetComponent<EnemySkeleton>();

            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
    }
}
