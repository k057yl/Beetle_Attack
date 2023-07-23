using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;

    private CharacterController _characterController;
    
    private void Awake()
    {
        Instantiate(_characterPrefab, transform.position, Quaternion.identity).GetComponent<CharacterController>();
    }
}
