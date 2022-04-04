
public class PowerupActive : Powerup
{
    public PowerupManager.ActivePowerup activePowerupType;
    public float length = 20f;

    public override void Pickup()
    {
        if( PowerupManager.activePowerup != PowerupManager.ActivePowerup.None ) return;
        powerupManager.MakeActivePowerup( activePowerupType, length );
        base.Pickup();
    }
}