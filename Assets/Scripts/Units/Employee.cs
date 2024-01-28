using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;
using static Unity.VisualScripting.Member;

public class Employee : MonoBehaviour, IUnit, ControllableObject
{
    public string collisionTag;
    [SerializeField]
    public float moveSpeed = 2f;
    public HealthController healthController;
    private Resistances armor;//Maybee Inject
    Dictionary<States, State> allStates;
    //SpriteRenderer.Flip!!!!!!!!!!!!!!!!!!!!
    [Inject]
    public StateController controller;
    public StateController<Data> controller2;

    public bool isControlled { get;  set ;}
    public delegate void OnClickDelegate(Employee controledPerson, StateController controller);
    public event OnClickDelegate OnClick;

    // Start is called before the first frame update
    private void Awake()
    {
        allStates = new Dictionary<States, State>
        {
            { States.Idle, new IdleState(this, controller) },
            { States.Chase, new ChaseState(this, controller) },
            { States.Attack, new AttackState(this, controller) },
            { States.Move, new MoveState(this, controller) }
        };
        State idle;
        allStates.TryGetValue(States.Idle, out idle);
        controller.Init(States.Idle, allStates);

        controller2 = new StateController<Data>();
        
        controller2.AddState<MoveStateT>(States.Move);//all other states
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
        controller.Do();
    }
    void ZeroHealthHandler()
    {
        Destroy(gameObject);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == collisionTag)
    //    {
    //        Health health = collision.gameObject.GetComponent<Health>();
    //        health.SetHealth(collisionDamage);
    //    }
    //}
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
       
    }

    public void TakeCommand(Vector3 position)
    {
        
    }
    public void SetMove()
    {
        controller.ChangeCurrentState(States.Move);
    }
    public void OnMouseUp()
    {
        Debug.Log("Click");
        if (isControlled)
        {
            isControlled = false;
            OnClick?.Invoke(null, controller);
        }
        else
        {
            isControlled = true;
            OnClick?.Invoke(this, controller);
        }
        
    }

    
    public void MoveToRoom(Room room, Vector3 position)
    {
        controller.ChangeCurrentState(States.Idle);
        transform.SetParent(room.transform);
        transform.localPosition = position;

    }

    public Transform GetTransform()
    {
        return transform;
    }

    public States GetCurrentState()
    {
        return controller.currentState;
    }
}
