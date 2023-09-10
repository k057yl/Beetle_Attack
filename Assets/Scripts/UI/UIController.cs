using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _totalAmmoText;
    [SerializeField] private Text _killedText;
    [SerializeField] private Text _healthText;

    private CharacterController _characterController;

    [Inject]
    private void Construct(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = currentAmmo.ToString();
        _totalAmmoText.text = maxAmmo.ToString();
    }
    
    public void UpdateKilledText()
    {
        _killedText.text = _characterController.Model.Kill.ToString();
    }
    
    public void UpdateHealthText()
    {
        _healthText.text = _characterController.Model.Health.ToString();
    }
}