using UnityEngine;

public class EnemyPath : Enemy
{
    [SerializeField] GameObject objetivo1, objetivo2;
    bool aObjetivo1;

    
    void Update()
    {
        Moverse();
        if (transform.position == objetivo1.transform.position)
        {
            aObjetivo1 = false;
        }
        if (transform.position == objetivo2.transform.position)
        {
            aObjetivo1 = true;
        }

    }

    public override void Moverse()
    {
        if (aObjetivo1)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo1.transform.position, speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo2.transform.position, speed);
        }

    }
}
