using UnityEngine;

public interface IWeaponFactory
{
    IWeapon CreatePistol();
    IWeapon CreateShotgun();
}
