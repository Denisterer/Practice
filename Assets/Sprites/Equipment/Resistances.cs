using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Resistance", order = 1)]
public class Resistances : ScriptableObject
{
    public float baseArmor;
    public List<Resistance> resistances;
    public bool canBeFrosen;
    public bool canBeLitOnFire;
    public bool CanBleed;
    public bool canBeCorroded;
    public Sprite armorSprite;
}
