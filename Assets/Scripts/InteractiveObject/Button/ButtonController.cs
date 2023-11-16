
using UnityEngine;
using Zenject;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;
    
    [Inject] private UIController _uiController;
    [Inject] private CharacterController _characterController;

    private bool HasEnoughBones()
    {
        return _characterController.Model.Bones >= Constants.TEN;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out CharacterController characterController))
        {
            if (HasEnoughBones() && _doorController.CollisionCount == 1)
            {
                _doorController.OpenDoorById(1);
                _characterController.Model.SpendBones(Constants.TEN);
                _uiController.UpdateBonesText();
            }
            if (HasEnoughBones() && _doorController.CollisionCount == 2)
            {
                _doorController.OpenDoorById(3);
                _characterController.Model.SpendBones(Constants.TEN);
                _uiController.UpdateBonesText();
            }
            else
            {
                Debug.Log("We need ten bones");
            }
        }
    }
}
