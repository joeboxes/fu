using System.Collections;
using System.Collections.Generic;

public class Weapon : Tool
{
    public enum Type
    {
        HandGun,
        Glock,
        Pistol,
        Revolver,
    }

    public Type type;
    public int totalCapacity;
    public int currentCapacity;
    public float damage;
    public float speed;
    public int shootCount; // sprayCount bulletCount
    public float spread; // radian angle 0-90 degrees [0,pi/2]
}
