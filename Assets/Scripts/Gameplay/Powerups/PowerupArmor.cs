
using UnityEngine;

public class PowerupArmor : Powerup
{
    public float armorMin = 20;
    public float armorMax = 33f;

    public override void Pickup()
    {
        player.AddArmor( Random.Range( armorMin, armorMax ) );
        base.Pickup();
    }
}
