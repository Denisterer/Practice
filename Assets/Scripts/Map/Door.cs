using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action<Door,IUnit> OnDoorEnter;
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        IUnit unit = otherCollider.GetComponent<IUnit>();
        if (unit != null) 
        {
                OnDoorEnter.Invoke(this,unit);   
        }
    }
    
}
