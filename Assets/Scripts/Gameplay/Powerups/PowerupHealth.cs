
using UnityEngine;

public class PowerupHealth : Powerup
{
    public float healthMin = 25f;
    public float healthMax = 100f;

    public override void Pickup()
    {
        player.AddHealth( Random.Range( healthMin, healthMax ) );
        base.Pickup();
    }
}
