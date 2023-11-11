using System;
using System.Collections.Generic;
using UnityEngine;

public class DoorListLevel_1 : MonoBehaviour
{
    public List<Doors> Level1Doors = new List<Doors>();

    public int CollisionCount = 0;
    

    public void OpenDoor(int doorIndex)
    {
        if (doorIndex >= 0 && doorIndex < Level1Doors.Count)
        {
            StartCoroutine(Level1Doors[doorIndex].OpenDoorCoroutine());
        }
    }

    public void CloseDoor(int doorIndex)
    {
        if (doorIndex >= 0 && doorIndex < Level1Doors.Count)
        {
            StartCoroutine(Level1Doors[doorIndex].CloseDoorCoroutine());
        }
    }
    
    public void DoorFirstRoom()
    {
        CloseDoor(0);
        CloseDoor(1);
        CollisionCount++;
    }
    
    public void DoorSecondRoom()
    {
        CloseDoor(2);
        CloseDoor(3);
        CollisionCount++;
    }
}
