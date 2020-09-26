using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static YorfLib.SingletonHelper;

public enum ButtonTypes
{
    A, B, Y, X, L, R, U, D
}

public class InputController : MonoBehaviour
{
    //private InputAction m_inputAction;

    private void Start()
    {
        //m_inputAction = GetComponent<InputAction>();
    }

    public void OnButtonPressed(InputAction.CallbackContext context)
    {
        //Get<GameController>().CurrentBeatCombination


        Debug.Log(context.control.name + " " + context.control.IsPressed());
    }

    private static Dictionary<string, ButtonTypes> m_buttonTypesMap = new Dictionary<string, ButtonTypes>
    {
        {"buttonWest", ButtonTypes.B }
    };
}
