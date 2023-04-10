using UnityEngine;
using UnityEngine.UI;

// Script for Enemy stats
public class EnemyGolem : MonoBehaviour
{
    [Header("General")]
    private float startHealth = 250f;
    [HideInInspector]
    public float health;
    public float speed = 3f;
    private bool finalDeath;

    [Header("Unity Setup")]
    public GameObject deathEffect;
    public Image healthBar;
    private Animator animator;
    public AudioSource hitAudio;
    public AudioSource dieAudio;

    void Start()
    {
        startHealth *= PlayerPrefs.GetInt("Difficulty", 1);
        health = startHealth;
        animator = GetComponent<Animator>();
        hitAudio.Stop();
        dieAudio.Stop();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
        {
            if (!hitAudio.isPlaying)
            {
                hitAudio.Play();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
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
}
