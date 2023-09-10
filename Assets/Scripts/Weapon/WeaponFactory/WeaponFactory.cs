public class WeaponFactory : IWeaponFactory
{
    public IWeapon CreatePistol()
    {
        return new Pistol();
    }

    public IWeapon CreateShotgun()
    {
        return new Shotgun();
    }
}
