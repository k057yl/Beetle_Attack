using UnityEngine;

public interface IInput
{
    Vector2 GetMovementInput();
    bool IsFireTriggered();
    bool IsSlotOneTriggered();
    bool IsSlotTwoTriggered();
    Vector3 GetCursorPosition();
    bool IsReloadTriggered();
}