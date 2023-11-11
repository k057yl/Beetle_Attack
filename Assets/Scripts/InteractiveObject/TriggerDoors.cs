using UnityEngine;

public class TriggerDoors : MonoBehaviour
{
    [SerializeField] private DoorListLevel_1 _doorList;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CharacterController>())
        {
            if (_doorList.CollisionCount == 0)
            {
                _doorList.DoorFirstRoom();
                Destroy(gameObject);
            }
            else if (_doorList.CollisionCount == 1)
            {
                _doorList.DoorSecondRoom();
                Destroy(gameObject);
            }
        }
    }
}
