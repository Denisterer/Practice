using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitFactory
{
    // Start is called before the first frame update
    IUnit Create(string unitType, IWeapon weapon, Resistances armor);
}
