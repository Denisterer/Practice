using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public DamageType type;
    public float damage;
    public float lifeTime = 2.0f;
    public IUnit source;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        IUnit target = otherCollider.GetComponent<IUnit>();
        if (target != null)
        {
            if ((source is Employee && target is Abnormality) ||(source is Abnormality && target is Employee))
            {
                target.TakeDamage(type, damage);
                Destroy(gameObject);
            }
        }
        
    }
}
