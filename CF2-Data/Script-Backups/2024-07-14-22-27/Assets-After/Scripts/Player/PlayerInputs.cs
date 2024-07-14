using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    [Header("Character Input Values")]
    public Vector2 movement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;

    public event Action onShootBtn;

    private void Update()
    {
        MoveInput(new Vector2(ControlFreak2.CF2Input.GetAxis("Horizontal"), ControlFreak2.CF2Input.GetAxis("Vertical")));
        ShootInput();
    }


    void MoveInput(Vector2 newMoveDirection)
    {
        movement = newMoveDirection;
    }

    void ShootInput()
    {
        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.E))
        {
            onShootBtn?.Invoke();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        //Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
