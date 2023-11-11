using System.Collections;
using UnityEngine;
using Zenject;

public class Doors : MonoBehaviour
{
    private const int SHIFT_DISTANCE = 5;
    
    private const float SHIFT_TIME = 1f;

    private const string DOOR_2 = "Door2";
    private const string DOOR_4 = "Door4";

    [Inject] private CharacterController _characterController;
    [Inject] private UIController _uiController;

    private bool _isMoving = false;

    private bool HasEnoughBones()
    {
        return _characterController.Model.Bones >= Constants.TEN;
    }

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

    public IEnumerator OpenDoorCoroutine()
    {
        if (_isMoving) yield break;

        if (!HasEnoughBones())
        {
            Debug.Log("Not enough bones, need 10 bones!!!");
            yield break;
        }

        yield return MoveDoorCoroutine(Vector3.up);
        
        _uiController.UpdateBonesText();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public IEnumerator CloseDoorCoroutine()
    {
        if (_isMoving) yield break;

        yield return MoveDoorCoroutine(Vector3.down);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<CharacterController>())
        {
            if (HasEnoughBones())
            {
                if (gameObject.tag == DOOR_2 ||  gameObject.tag == DOOR_4)
                {
                    StartCoroutine(OpenDoorCoroutine());
                    _characterController.Model.SpendBones(Constants.TEN);
                    _uiController.UpdateBonesText();
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else
                {
                    Debug.Log("This door is passive.");
                }
            }
            else
            {
                Debug.Log("Not enough bones, need 10 bones!!!");
            }
        }
    }
}