using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Abnormality : MonoBehaviour, IUnit
{
    public event Action<GameObject> OnClick;
    public event Action<Room> SwitchRoom;

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

    public States GetCurrentState()
    {
        return controller2.currentState;
    }

    public void MoveToPosition(Vector3 position)
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
        Destroy(gameObject);
    }
    void Start()
    {
        healthController = new HealthController(100f);
        healthController.OnZeroHealth += ZeroHealthHandler;
    }

    // Update is called once per frame
    void Update()
    {
        controller2.Do();
    }
    public void OnMouseUp()
    {
        Debug.Log("ClickAbno");
        OnClick?.Invoke(this.GameObject());

    }
    public void AddTarget(IUnit target)
    {
        if (target is Employee)
        {
            controller2.data.unitList.Add(target);
        }
    }

    public void RemoveTarget(IUnit target)
    {
        if (controller2.data.unitList.Contains(target))
        {
            controller2.data.unitList.Remove(target);
        }
    }
}
