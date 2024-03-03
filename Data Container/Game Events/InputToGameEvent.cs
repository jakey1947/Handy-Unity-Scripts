using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputToGameEvent : MonoBehaviour
{
    public void RaiseEvent(GameEvent gameEvent, InputAction.CallbackContext context)
    {
        if(!context.performed)
            return;

        gameEvent.Raise();
    }
}