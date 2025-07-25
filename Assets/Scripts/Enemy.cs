using UnityEngine;
using UnityEngine.VFX;

public abstract class Enemy : MonoBehaviour, IRecibirDa�o, IKnockBack
{
    [SerializeField] protected float life;
    [SerializeField] protected float speed;
    protected Rigidbody rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Moverse();
    public void RecibirDa�o(float da�o)
    {
        life -= da�o;
        if (life <= 0)
        {
            GameManager.defeatedEnemies++;
            Debug.Log(GameManager.defeatedEnemies);
            Destroy(gameObject);
            
        }
    }

    public void Execute(Transform knockbackSource)
    {
        Vector3 dir = (transform.position - knockbackSource.transform.position).normalized;
        rb.AddForce(dir, ForceMode.Impulse);
    }
}
