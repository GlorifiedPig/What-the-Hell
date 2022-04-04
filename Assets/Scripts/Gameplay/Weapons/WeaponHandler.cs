
using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject activeWeapon;

    public static event Action<GameObject, GameObject> WeaponSwitched = ( newWeapon, oldWeapon ) => { };

    public void SwitchToWeapon( GameObject weapon )
    {
        if( !weapons.Contains( weapon ) || activeWeapon == weapon ) return;

        GameObject oldWeapon = activeWeapon;
        activeWeapon = weapon;
        weapon.SetActive( true );
        oldWeapon.SetActive( false );
        WeaponSwitched.Invoke( weapon, oldWeapon );
    }

    public void Update()
    {
        if( !Player.alive ) return;
        if( Input.GetKeyDown( KeyCode.Alpha1 ) && weapons[0] ) SwitchToWeapon( weapons[0] );
        else if( Input.GetKeyDown( KeyCode.Alpha2 ) && weapons[1] ) SwitchToWeapon( weapons[1] );
        else if( Input.GetKeyDown( KeyCode.Alpha3 ) && weapons[2] ) SwitchToWeapon( weapons[2] );
        else if( Input.GetKeyDown( KeyCode.Alpha4 ) && weapons[3] ) SwitchToWeapon( weapons[3] );
    }
}
