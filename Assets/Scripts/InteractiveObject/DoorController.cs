using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform[] _doorSpawnPoints;

    [Inject] private DoorFactory _doorFactory;
    //[Inject] private CharacterController _characterController;
    
    [SerializeField] private int _collisionCount = 0;

    public int CollisionCount => _collisionCount;

    private List<Doors> _doorsList = new List<Doors>();//-------------
    
    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            var door = _doorFactory.Create();
            
            door.Initialize(i);//-------------
            _doorsList.Add(door);//-----------
            
            if (_doorSpawnPoints.Length > i)
            {
                door.transform.position = _doorSpawnPoints[i].position;
            }
            else
            {
                Debug.LogError($"No spawn point specified for door {i + 1}!");
            }
        }
    }

    public void CollisionCountPlus()
    {
        _collisionCount++;
    }
    
    public void OpenDoorById(int doorId)
    {
        var door = _doorsList.Find(d => d.DoorId == doorId);
        if (door != null)
        {
            door.Open();
        }
        else
        {
            Debug.LogError($"Door with ID {doorId} not found.");
        }
    }

    public void CloseDoorById(int doorId)
    {
        var door = _doorsList.Find(d => d.DoorId == doorId);
        if (door != null)
        {
            door.Close();
        }
        else
        {
            Debug.LogError($"Door with ID {doorId} not found.");
        }
    }
}
