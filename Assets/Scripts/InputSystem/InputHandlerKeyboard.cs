using UnityEngine;

public class InputHandlerKeyboard : IInput
{
    private NewInput _newInput;

    public InputHandlerKeyboard()
    {
        _newInput = new NewInput();
        _newInput.Enable();
    }

    public Vector2 GetMovementInput()
    {
        return _newInput.Gameplay.Movement.ReadValue<Vector2>();
    }

    public bool IsFireTriggered()
    {
        return _newInput.Gameplay.Fire.triggered;
    }
    
    public bool IsSlotOneTriggered()
    {
        return _newInput.Gameplay.SlotOne.triggered;
    }
    
    public bool IsSlotTwoTriggered()
    {
        return _newInput.Gameplay.SlotTwo.triggered;
    }
    
    public Vector3 GetCursorPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool IsReloadTriggered()
    {
        return _newInput.Gameplay.Reload.triggered;
    }
/*
    public bool NoMousePresent()
    {
        return Input.mousePresent;
    }
*/    
}