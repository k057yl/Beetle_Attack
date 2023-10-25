public interface IAmmoSystem
{
    int GetCurrentAmmo();
    int GetCurrentMagazineAmmo();
    
    void UseAmmo();
    void AmmoKitFifty();
    void AmmoKitTen();
    void Reload();
    void ReloadButton();
    bool CanReload();
}