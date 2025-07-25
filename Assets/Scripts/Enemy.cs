using UnityEngine;
using UnityEngine.VFX;

public abstract class Enemy : MonoBehaviour, IRecibirDaño, IKnockBack
{
    [SerializeField] protected float life;
    /*[SerializeField] protected float damage;*/
    [SerializeField] protected float speed;
    //[SerializeField] protected VisualEffect boom;
    //[SerializeField] protected float boomTime;
    protected Rigidbody rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Moverse();
    public void RecibirDaño(float daño)
    {
        life -= daño;
        if (life <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemigo Muerto");
            GameManager.defeatedEnemies++;
        }
    }

    public void Execute(Transform knockbackSource)
    {
        Vector3 dir = (transform.position - knockbackSource.transform.position).normalized;
        rb.AddForce(dir, ForceMode.Impulse);
    }
}
