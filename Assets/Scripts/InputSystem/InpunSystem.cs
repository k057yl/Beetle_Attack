using UnityEngine;

public class InpunSystem
{
    private static InpunSystem _instance;
    public static InpunSystem Instance => _instance ?? (_instance = new InpunSystem());

    private NewInput _newInput;
    

    private InpunSystem()
    {
        _newInput = new NewInput();
        _newInput.Enable();
    }

    public Vector2 GetMovementInput()
    {
        return _newInput.Gameplay.Movement.ReadValue<Vector2>();
    }
    /*
    public bool GetTouch()
    {
        return _newInput.Gameplay.Touch.triggered;
    }

    public Vector2 GetTouchPosition()
    {
        
        if (_newInput.Gameplay.Touch.triggered)
        {
            int touchCount = Input.touchCount;
            if (touchCount > Constants.ZERO)
            {
                Touch touch = Input.GetTouch(Constants.ZERO);
                
                if (touch.position.x >= Constants.ZERO && touch.position.x <= Screen.width && touch.position.y >= Constants.ZERO && touch.position.y <= Screen.height)
                {
                    return touch.position;
                }
            }
        }

        return Vector2.zero;
    }
    */
}
