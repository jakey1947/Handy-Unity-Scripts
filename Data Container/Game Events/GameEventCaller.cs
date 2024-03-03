using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Game Events/Game Event Caller")]
public class GameEventCaller : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;

    public void RaiseEvent()
    {
        gameEvent.Raise();
    }

    public void RaiseEvent(InputAction.CallbackContext context)
    {
        if(!context.performed)
            return;        

        gameEvent.Raise();
    }

    public void RaiseEvent(GameEvent gameEvent)
    {
        gameEvent.Raise();
    }
}