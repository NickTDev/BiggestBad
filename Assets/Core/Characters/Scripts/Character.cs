using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public int ID;
    public string Name;
    public int HitPoints;
    public int EnergyPoints;
    public int Attack;
    public int Defense;
    public int Speed;
    public string MovementType;

    public Character(Character d)
    {
        ID = d.ID;
        Name = d.Name;
        HitPoints = d.HitPoints;
        EnergyPoints = d.EnergyPoints;
        Attack = d.Attack;
        Defense = d.Defense;
        Speed = d.Speed;
        MovementType = d.MovementType;
    }

    public Character()
    {
        ID = 0;
        Name = "blank";
        HitPoints = 0;
        Attack = 0;
        Defense = 0;
        Speed = 0;
        MovementType = "Ground";
    }

}
