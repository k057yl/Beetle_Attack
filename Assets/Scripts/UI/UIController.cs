using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    private const float ZERRO = 0f;
    private const float TIME_RELOAD_PISTOL = 2f;
    private const float TIME_RELOAD_SHOTGUN = 3f;
    
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _totalAmmoText;
    [SerializeField] private Text _boneText;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _killedText;
    
    [SerializeField] private Image _coolDownIcon;
    

    [Inject] private CharacterController _characterController;

    
    
    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = currentAmmo.ToString();
        _totalAmmoText.text = maxAmmo.ToString();
    }
    
    public void UpdateBonesText()
    {
        _boneText.text = _characterController.Model.Bones.ToString();
    }
    
    public void UpdateKilledText()
    {
        _killedText.text = _characterController.Model.Kill.ToString();
    }
    
    public void UpdateHealthText(int health)
    {
        _healthText.text = health.ToString();
    }

    public void CoolDownPistolImage()
    {
        StartCoroutine(FillCooldownOverTime(TIME_RELOAD_PISTOL));
    }
    
    public void CoolDownShotgunImage()
    {
        StartCoroutine(FillCooldownOverTime(TIME_RELOAD_SHOTGUN));
    }
    
    private IEnumerator FillCooldownOverTime(float duration)
    {
        float elapsedTime = ZERRO;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _coolDownIcon.fillAmount = elapsedTime / duration;
            yield return null;
        }
    }
}