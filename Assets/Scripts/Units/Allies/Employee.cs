using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
using static Unity.VisualScripting.Member;

public class Employee : MonoBehaviour, IUnit, ControllableObject
{
    public HealthController healthController;
    private Resistances armor;//Maybee Inject
    //SpriteRenderer.Flip!!!!!!!!!!!!!!!!!!!!

    public StateController<Data> controller2;

    public bool isControlled { get;  set ;}

    public float Speed { get; set; } = 2f;
    public bool canBeDistracted { get; set; } = false;
    public bool canSwitchRoom { get; set; } = true;
    public bool canSwitchRoomWithoutCommand { get; set; } = false;
    public bool canMoveAround { get; set; } = true;

    public delegate void OnClickDelegate(Employee controledPerson);
    public event OnClickDelegate OnClick;

    // Start is called before the first frame update
    private void Awake()
    {
        controller2 = new StateController<Data>();
        
        controller2.AddState<MoveStateT>(States.Move);//all other states
        controller2.AddState<IdleStateT>(States.Idle);//all other states
        controller2.AddState<ChaseStateT>(States.Chase);//all other states
        controller2.AddState<AttackStateT>(States.Attack);//all other states
        controller2.data = new Data { unit=this};
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
    void ZeroHealthHandler()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(DamageType damageType, float damageValue)
    {
        foreach(Resistance resistance in armor.resistances)
        {
            if(resistance.type == damageType)
            {
                damageValue *= resistance.value; break;
            }
        }
        if(damageValue > 0)
        {
            damageValue -= armor.baseArmor;
            if(damageValue <= 0)
                {
                    damageValue = 0;
                }
        }
        
        healthController.ChangeHealth(damageValue);
    }

    public void TakeCommand(GameObject target)
    {
        if(target.GetComponent<Abnormality>())
        {
            controller2.data.currentTarget = target.GetComponent<Abnormality>();
            controller2.ChangeCurrentState(States.Chase);
        }
        

    }

    public void TakeCommand(Vector3 position)
    {
        
    }
    public void SetMove(LinkedList<Room> rooms, float finalPositionX)
    {
        controller2.data.finalPositionX = finalPositionX;
        controller2.data.path = rooms;
        controller2.data.currentRoom = rooms.First;
        if(rooms.Count <= 1)
        {
            controller2.data.currentDestination = controller2.data.currentRoom.Value;
        }
        else
        {
            controller2.data.currentDestination = controller2.data.currentRoom.Next.Value;
        }

        controller2.ChangeCurrentState(States.Move);
    }
    public void OnMouseUp()
    {
        Debug.Log("Click");
        if (isControlled)
        {
            isControlled = false;
            OnClick?.Invoke(null);
        }
        else
        {
            isControlled = true;
            OnClick?.Invoke(this);
        }
        
    }

    
    public void MoveToPosition(Vector3 position)
    {
        transform.SetParent(controller2.data.currentDestination.transform);
        transform.localPosition = position;
        controller2.OnDoorEnter();        
    }
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

    public void AddTarget(IUnit target)
    {
        if (target is Abnormality)
        {
            controller2.data.unitList.Add(target);
        }
    }

    public void RemoveTarget(IUnit target)
    {
        if(controller2.data.unitList.Contains(target))
        {
            controller2.data.unitList.Remove(target);
        }
    }
}