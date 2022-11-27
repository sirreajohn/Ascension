using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    public void quit_app(InputAction.CallbackContext callback)
    {
        Application.Quit();
    }
}


