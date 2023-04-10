using UnityEngine;

[RequireComponent(typeof(EnemySkeleton))]
public class EnemySkeletonMovement : MonoBehaviour
{
    private Transform target;
    private EnemySkeleton enemy;
    private Animator animator;
    public AudioSource walkAudio;
    private int waypointIndex = 0;
    private float idleTimer = 10;

    [HideInInspector]
    public bool move = true;

    void Start()
    {
        enemy = GetComponent<EnemySkeleton>();
        animator = GetComponent<Animator>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        animator.SetBool("Walking", move);

        if (move)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        }

        if ((Vector3.Distance(transform.position, target.position) <= 0.4f) && waypointIndex == 0)
        {
            MoveToRandomWaypoint();
        }

        if ((Vector3.Distance(transform.position, target.position) <= 0.2f) && waypointIndex > 0)
        {
            walkAudio.Stop();
            move = false;
        }

        if (move == false)
        {
            idleTimer -= Time.deltaTime;

            if (idleTimer <= 0)
            {
                animator.SetTrigger("Victory");
                idleTimer = 10;
            }
        }
    }

    void MoveToRandomWaypoint()
    {
        waypointIndex = Random.Range(1, 4);
        target = Waypoints.points[waypointIndex];
    }
}
