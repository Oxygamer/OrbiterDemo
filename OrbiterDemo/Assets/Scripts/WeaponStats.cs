using System.Collections;
using System.Collections.Generic;

public class WeaponStats {

    public int ID;
    public float CooldownTime = 1f;
    public float Mass = 1f;
    public float InitialSpeed = 30f;
    public float Damage = 1f;
    public WeaponType Type;
}

public enum WeaponType
{
    Fast,
    Double,
    Heavy,
    SpaceTrash,
}
