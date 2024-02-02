using UnityEngine;

public interface IUnit 
{
    float Speed { get; }
    bool canBeDistracted { get; set; }
    bool canSwitchRoom { get; set; }
    bool canMoveAround {  get; set; }
    Transform GetTransform();
    States GetCurrentState();
    Room GetDestination();
    void AddTarget(IUnit target);
    void RemoveTarget(IUnit target);
    void TakeDamage(DamageType damageType, float damageValue);

    void MoveToPosition(Vector3 position);

}
