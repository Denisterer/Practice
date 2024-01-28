using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    GameObject unitPrefab;
    IUnitFactory unitFactory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        if (unitPrefab != null)
        {
            //unitFactory.create(unitPrefab)
        }
    }
}
