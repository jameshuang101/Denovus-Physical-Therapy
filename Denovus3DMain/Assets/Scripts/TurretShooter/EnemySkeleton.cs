using UnityEngine;
using UnityEngine.UI;

// Script for Enemy stats
public class EnemySkeleton : MonoBehaviour
{
    [Header("General")]
    private float startHealth = 200f;
    [HideInInspector]
    public float health;
    public float speed = 3f;
    private bool finalDeath;
    private int randomizer;
    [HideInInspector]
    public int[] limbValues;
    [HideInInspector]
    public int counter = 0;

    [Header("Unity Setup")]
    public GameObject deathEffect;
    public Image healthBar;
    private Animator animator;
    public AudioSource hitAudio;
    public AudioSource dieAudio;
    public Transform leftArm;
    public Transform leftArmPrefab;
    public Transform rightArm;
    public Transform rightArmPrefab;
    public Transform leftLeg;
    public Transform leftLegPrefab;
    public Transform rightLeg;
    public Transform rightLegPrefab;

    void Start()
    {
        health = startHealth;
        animator = GetComponent<Animator>();
        hitAudio.Stop();
        dieAudio.Stop();

        randomizer = Random.Range(1, 12);

        if (randomizer == 1)
            limbValues = new int[] { 1, 2, 3, 4 };
        else if (randomizer == 2)
            limbValues = new int[] { 1, 3, 2, 4 };
        else if (randomizer == 3)
            limbValues = new int[] { 2, 1, 3, 4 };
        else if (randomizer == 4)
            limbValues = new int[] { 2, 3, 1, 4 };
        else if (randomizer == 5)
            limbValues = new int[] { 3, 1, 2, 4 };
        else if (randomizer == 6)
            limbValues = new int[] { 3, 2, 1, 4 };
        else if (randomizer == 7)
            limbValues = new int[] { 4, 1, 2, 3 };
        else if (randomizer == 8)
            limbValues = new int[] { 4, 2, 1, 3 };
        else if (randomizer == 9)
            limbValues = new int[] { 2, 1, 4, 3 };
        else if (randomizer == 10)
            limbValues = new int[] { 2, 4, 1, 3 };
        else if (randomizer == 11)
            limbValues = new int[] { 1, 2, 4, 3 };
        else
            limbValues = new int[] { 1, 4, 2, 3 };
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage01"))
        {
            if (!hitAudio.isPlaying)
            {
                LoseLimb();
                hitAudio.Play();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkeletonOutlaw@Dead01"))
        {
            if (!finalDeath)
            {
                dieAudio.Play();
                finalDeath = true;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }

        animator.SetTrigger("GettingHit");
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        effect.transform.Translate(0f, 1f, 0f);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }

    public void FallAndDie()
    {
        dieAudio.Play();
    }

    public void LoseLimb()
    {
        if (limbValues[counter] == 1)
        {
            leftArm.transform.localScale = Vector3.zero;
            Transform la = Instantiate(leftArmPrefab, leftArm.transform.position, leftArm.transform.rotation);
            Destroy(la.gameObject, 2f);
        }
        else if (limbValues[counter] == 2)
        {
            rightArm.transform.localScale = Vector3.zero;
            Transform ra = Instantiate(rightArmPrefab, rightArm.transform.position, rightArm.transform.rotation);
            ra.transform.Rotate(0f, 0f, 180f);
            Destroy(ra.gameObject, 2f);

        }
        else if (limbValues[counter] == 3)
        {
            leftLeg.transform.localScale = Vector3.zero;
            Transform ll = Instantiate(leftLegPrefab, leftLeg.transform.position + new Vector3(.65f, -.8f, 0f), Quaternion.Euler(0f, 0f, 0f));
            ll.transform.Rotate(-20f, 90f, 0f);
            Destroy(ll.gameObject, 2f);
        }
        else
        {
            rightLeg.transform.localScale = Vector3.zero;
            Transform rl = Instantiate(rightLegPrefab, rightLeg.transform.position + new Vector3(.65f, -.8f, 0f), Quaternion.Euler(0f, 0f, 0f));
            rl.transform.Rotate(-20f, 90f, 0f);
            Destroy(rl.gameObject, 2f);
        }

        counter++;
    }
}
