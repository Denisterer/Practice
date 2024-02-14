using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Abnormality : MonoBehaviour, IUnit
{
    public event Action<Abnormality> OnClick;
    public event Action<Room> SwitchRoom;
    public event Action<IUnit> OnDestroy;
    public event Action<IUnit> OnRandomMove;


    public IWeapon weapon;

    public HealthController healthController;
    private Resistances armor;
    public StateController<Data> controller2;
    public float Speed { get; set; } = 2f;

    public bool canBeDistracted { get; set; } = false;
    public bool canSwitchRoom { get; set; } = true;
    public bool canMoveAround { get; set; } = true;

    public Room GetDestination()
    {
        return controller2.data.currentDestination;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    public Rigidbody2D GetRigidbody()
    {
        return GetComponent<Rigidbody2D>();
    }

    public States GetCurrentState()
    {
        return controller2.currentState;
    }

    public void MoveToRoom(Vector3 position)
    {
        controller2.data.unitList.Clear();
        controller2.data.unitList = controller2.data.currentDestination.GetComponentsInChildren<Employee>().ToList<IUnit>();
        transform.SetParent(controller2.data.currentDestination.transform);
        transform.localPosition = position;
        controller2.OnDoorEnter();
    }

    public void TakeDamage(DamageType damageType, float damageValue)
    {
        foreach (Resistance resistance in armor.resistances)
        {
            if (resistance.type == damageType)
            {
                damageValue *= resistance.value; break;
            }
        }
        if (damageValue > 0)
        {
            damageValue -= armor.baseArmor;
            if (damageValue <= 0)
            {
                damageValue = 0;
            }
        }

        healthController.ChangeHealth(damageValue);
    }

    private void Awake()
    {
        controller2 = new StateController<Data>();

        controller2.AddState<MoveStateT>(States.Move);//all other states
        controller2.AddState<IdleStateT>(States.Idle);//all other states
        controller2.AddState<ChaseStateT>(States.Chase);//all other states
        controller2.AddState<AttackStateT>(States.Attack);//all other states
        controller2.data = new Data { unit = this };
    }
    void ZeroHealthHandler()
    {
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }

    void Start()
    {
        healthController = new HealthController(100f);
        healthController.OnZeroHealth += ZeroHealthHandler;
        controller2.ChangeCurrentState(States.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        controller2.Do();
    }
    public void OnMouseUp()
    {
        Debug.Log("ClickAbno");
        OnClick?.Invoke(this);

    }
    public void AddTarget(IUnit target)
    {
        if (target is Employee)
        {
            controller2.data.unitList.Add(target);
            target.OnDestroy += RemoveTarget;
            target.OnDestroy += LoseTarget;
            if (controller2.data.currentTarget == null)
            {
                SetAttack(target);
            }
        }
    }

    public void RemoveTarget(IUnit target)
    {
        if (controller2.data.unitList.Contains(target))
        {
            controller2.data.unitList.Remove(target);
        }
    }
    public void LoseTarget(IUnit target)
    {
        Debug.LogError("LosingTarget");
        if (controller2.data.currentTarget == target)
        {
            Debug.LogError("TargetLost");

            if (controller2.data.unitList.Count > 0)
            {
                SetAttack(controller2.data.unitList[0]);
                RemoveTarget(controller2.data.currentTarget);
            }
            else
            {
                controller2.data.currentTarget = null;
            }
        }
    }

    public float GetAttackRange()
    {
        return weapon.GetRange();
    }


    public void PerformAttack(Vector3 target)
    {
        weapon.Shoot(target);

    }

    public void SetMove(LinkedList<Room> rooms, float finalPositionX)
    {
        if (rooms == null)
        {

        }
        controller2.data.finalPositionX = finalPositionX;
        controller2.data.path = rooms;
        controller2.data.currentRoom = rooms.First;
        if (rooms.Count <= 1)
        {
            controller2.data.currentDestination = controller2.data.currentRoom.Value;
        }
        else
        {
            controller2.data.currentDestination = controller2.data.currentRoom.Next.Value;
        }

        controller2.ChangeCurrentState(States.Move);
    }

    public void SetAttack(IUnit target)
    {
        controller2.data.currentTarget = target;
        controller2.ChangeCurrentState(States.Attack);
    }

    public void SetIdle()
    {
        controller2.ChangeCurrentState(States.Idle);
    }

    public void AddWeapon(IWeapon weapon)
    {
        this.weapon = weapon;
        weapon.GetTransform().SetParent(this.transform, false);
    }
    public void AddArmor(Resistances armor)
    {
        this.armor = armor;
    }
    public void MakeRandomMove()
    {
        OnRandomMove?.Invoke(this);
    }
    public Room GetCurrentRoom()
    {
        return GetComponentInParent<Room>();
    }
    public void SpriteFlip(bool isFlipped)
    {
        GetComponent<SpriteRenderer>().flipX = isFlipped;
    }
}
