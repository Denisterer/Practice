using UnityEngine;

public interface IUnit 
{
    Transform GetTransform();
    States GetCurrentState();
    void TakeDamage(DamageType damageType, float damageValue);

    void MoveToRoom(Room room,Vector3 position);

}
