using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;

    public CharacterController CreateCharacter()
    {
        GameObject characterObject = Instantiate(_characterPrefab);
        CharacterController characterController = characterObject.GetComponent<CharacterController>();
        return characterController;
    }
}