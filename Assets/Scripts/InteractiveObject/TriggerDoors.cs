using UnityEngine;

public class TriggerDoors : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CharacterController>())
        {
            if (_doorController.CollisionCount == 0)
            {
                _doorController.CloseDoorById(0);
                _doorController.CloseDoorById(1);
                
                _doorController.CollisionCountPlus();
                
                Destroy(gameObject);
            }
            else if (_doorController.CollisionCount == 1)
            {
                _doorController.CloseDoorById(2);
                _doorController.CloseDoorById(3);
                
                _doorController.CollisionCountPlus();
                
                Destroy(gameObject);
            }
        }
    }
}
