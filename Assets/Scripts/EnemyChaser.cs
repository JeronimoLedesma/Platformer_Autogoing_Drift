using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : Enemy
{
    public NavMeshAgent agent;
    private Transform player;

    public LayerMask groundedMask, playerMask;

    [Header("To See")]
    [SerializeField] float sightRange;
    private bool seesPlayer;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    
    void Update()
    {
        if (!GameManager.pause)
        {
            seesPlayer = Physics.CheckSphere(transform.position, sightRange, playerMask);

            if (seesPlayer) Moverse();
        }
        
    }

    public override void Moverse()
    {
        agent.SetDestination(player.position);
    }


}
