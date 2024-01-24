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
    private float maxHealth=100f;
    public float moveSpeed = 2f;
    private float currHealth;
     Dictionary<DamageType, float> Vulnerabilities= new Dictionary<DamageType, float>
    {
        { DamageType.Fire, 1.2f },
        { DamageType.Ice, 1.2f },
        { DamageType.Physical, 1f },
        { DamageType.Corrosion, 0.8f }
    };
    Dictionary<States, State> allStates;

    [Inject]
    public StateController controller;
    private State idleState;
    private State chaseState;
    private State attackState;
    private State moveState;
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
    }
    void Start()
    {
        State idle;
        allStates.TryGetValue(States.Idle, out idle);
        controller.Init(idle, allStates);
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        controller.currentState.Do();
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
        if (Vulnerabilities.ContainsKey(damageType))
        {
            currHealth -= damageValue * Vulnerabilities[damageType];
        }
        else
        {
            currHealth -= damageValue;
        }
    }

    public void TakeCommand(GameObject target)
    {
       
    }

    public void TakeCommand(Vector3 position)
    {
        
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

    
    public void MoveToRoom(Room room)
    {
        transform.SetParent(room.transform);
    }
}
