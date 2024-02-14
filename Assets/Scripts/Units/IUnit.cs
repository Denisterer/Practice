using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit 
{
    public event Action<IUnit> OnDestroy;
    public event Action<IUnit> OnRandomMove;

    float Speed { get; }
    bool canBeDistracted { get; set; }
    bool canSwitchRoom { get; set; }
    bool canMoveAround {  get; set; }
    Transform GetTransform();
    Rigidbody2D GetRigidbody();
    States GetCurrentState();
    Room GetDestination();
    Room GetCurrentRoom();
    float GetAttackRange();
    void AddTarget(IUnit target);
    void RemoveTarget(IUnit target);
    void TakeDamage(DamageType damageType, float damageValue);
    void PerformAttack(Vector3 target);
    void SetMove(LinkedList<Room> rooms, float finalPosition);
    void SetAttack(IUnit target);
    void SetIdle();
    void AddWeapon(IWeapon weapon);
    void AddArmor(Resistances armor);

    void MoveToRoom(Vector3 position);
    void MakeRandomMove();
    void SpriteFlip(bool isFlipped);

}
