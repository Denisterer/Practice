using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Resistance", order = 1)]
public class Resistances : ScriptableObject
{
    public float baseArmor;
    public Resistance iceResistance;
    public Resistance fireResistance;
    public Resistance physicResistance;
    public Resistance corrosionResistance;
    public bool canBeFrosen;
    public bool canBeLitOnFire;
    public bool CanBleed;
    public bool canBeCorroded;
}
