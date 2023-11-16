using System.Collections;
using UnityEngine;
using Zenject;

public class Doors : MonoBehaviour
{
    public class Factory : PlaceholderFactory<int, Doors> { }
    private const int SHIFT_DISTANCE = 5;
    
    private const float SHIFT_TIME = 1f;
    
    private bool _isMoving = false;
//--------------
    private int _doorId;

    public int DoorId => _doorId;

    public void Initialize(int doorId)
    {
        _doorId = doorId;
    }
//----------------

    private IEnumerator MoveDoorCoroutine(Vector3 direction)
    {
        _isMoving = true;

        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + direction * SHIFT_DISTANCE;

        while (elapsedTime < SHIFT_TIME)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / SHIFT_TIME));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos + direction * SHIFT_DISTANCE;

        _isMoving = false;
    }
    
    private IEnumerator OpenDoorCoroutine()//--------------
    {
        if (_isMoving) yield break;

        yield return MoveDoorCoroutine(Vector3.up);
        
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private IEnumerator CloseDoorCoroutine()//--------------
    {
        if (_isMoving) yield break;

        yield return MoveDoorCoroutine(Vector3.down);
    }
//-----------
    public void Open()
    {
        StartCoroutine(OpenDoorCoroutine());
    }
    
    public void Close()
    {
        StartCoroutine(CloseDoorCoroutine());
    }
}