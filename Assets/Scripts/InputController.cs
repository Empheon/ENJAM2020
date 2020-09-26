using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static YorfLib.SingletonHelper;

public enum ButtonType
{
    A, B, Y, X, L, R, U, D, NONE
}

public class InputController : MonoBehaviour
{

    private void Start()
    {
        //m_inputAction = GetComponent<InputAction>();
    }

    public void OnButtonPressed(InputAction.CallbackContext context)
    {
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
