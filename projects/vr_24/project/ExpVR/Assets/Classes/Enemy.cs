using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public enum Type
    {
        Basic,
        Shooting,
        Evading,
        Shielded,
    }

    public Type type;
    public float healthCurrent;
    public float healthTotal;
    public int points;
    
}
