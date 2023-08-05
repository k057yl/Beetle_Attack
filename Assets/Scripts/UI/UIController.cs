using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _totalAmmoText;
    [SerializeField] private Text _killedText;
    [SerializeField] private Text _healthText;

    
    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = currentAmmo.ToString();
        _totalAmmoText.text = maxAmmo.ToString();
    }
    
    public void UpdateKilledText(int killed)
    {
        _killedText.text = killed.ToString();
    }
    
    public void UpdateHealthText(int health)
    {
        _healthText.text = health.ToString();
    }
}