public interface IBullet
{
    DamageType damageType { get; set; }
    float damageValue { get; set; }

    void DealDamage(DamageType damageType, float damageValue);
}
