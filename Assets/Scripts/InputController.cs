using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using static YorfLib.SingletonHelper;

public enum ButtonType
{
    A, B, Y, X, L, R, U, D, NONE
}

public class InputController : MonoBehaviour
{
    public bool isXbox;

    private void Start()
    {
        InitSingleton(this);
        isXbox = InputSystem.devices.Any((x) => x.device is XInputController);
        //if (InputDevice. is XInputController)
        //    /* Xbox gamepad */
        //    ;
        //else if (gamepad is DualShockGamepad)
        //    /* PlayStation gamepad */
        //    ;
    }

    public void OnButtonPressed(InputAction.CallbackContext context)
    {
        //isXbox = context.control.device is XInputController;

        if (context.control.IsPressed() && Get<GameController>().CurrentBeatCombination != null)
        {
            Get<GameController>().CurrentBeatCombination.ReceiveInput(m_buttonTypesMap[context.control.name]);
        }
    }

    private static Dictionary<string, ButtonType> m_buttonTypesMap = new Dictionary<string, ButtonType>
    {
        {"buttonWest", ButtonType.X},
        {"buttonEast", ButtonType.B},
        {"buttonNorth", ButtonType.Y},
        {"buttonSouth", ButtonType.A},
        {"buttonUp", ButtonType.U},
        {"buttonDown", ButtonType.D},
        {"buttonLeft", ButtonType.L},
        {"buttonRight", ButtonType.R},
    };
}
