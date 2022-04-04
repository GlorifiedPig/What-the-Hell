using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public RoundManager roundManager;
    public WeaponBase weapon;

    public TMP_Text roundDisplay;
    public TMP_Text ammoText;

    private void OnEnable() => WeaponHandler.WeaponSwitched += WeaponSwitched;
    private void OnDisable() => WeaponHandler.WeaponSwitched -= WeaponSwitched;

    public void Update()
    {
        if( roundManager.round >= 1 ) roundDisplay.text = "Round " + roundManager.round;
        ammoText.text = weapon.ammo + " / " + weapon.clipSize; 
    }

    private void WeaponSwitched( GameObject newWeapon, GameObject oldWeapon )
    {
        weapon = newWeapon.GetComponent<WeaponBase>(); // This is VERY BAD PRACTICE, BUT THERE IS 1 HOUR UNTIL SUBMISSION I'LL CACHE THIS LATER AND PAIR WEAPONS WITH THEIR BASES PROPERLY.
    }
}
