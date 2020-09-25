using UnityEngine;
using UnityEngine.EventSystems;

namespace YorfLib
{
	public static class InputHelper
	{
		public static bool IsTouchDownNoUI(int touchIndex = 0)
		{
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if(Input.touchCount <= touchIndex)
            {
                return false;
            }
		    return Input.touchCount > 0 && Input.GetTouch(touchIndex).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(touchIndex).fingerId);
#else
            return Input.GetMouseButtonDown(touchIndex) && !EventSystem.current.IsPointerOverGameObject();
#endif
		}

        public static bool IsTouchUp(int touchIndex = 0)
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if(Input.touchCount <= touchIndex)
            {
                return false;
            }
		    return Input.touchCount > 0 && Input.GetTouch(touchIndex).phase == TouchPhase.Ended;
#else
            return Input.GetMouseButtonUp(touchIndex);
#endif
        }

        public static Vector2 GetTouchPosition(int touchIndex = 0)
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if(Input.touchCount <= touchIndex)
            {
                return Vector2.zero;
            }
		    return Input.GetTouch(touchIndex).position;
#else
            return Input.mousePosition;
#endif
        }

        public static bool IsTouchNoUI(int touchIndex = 0)
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if(Input.touchCount <= touchIndex)
            {
                return false;
            }
		    TouchPhase phase = Input.GetTouch(touchIndex).phase;
            bool isValidTouchPhase = phase <= TouchPhase.Stationary;
            return Input.touchCount > 0 && isValidTouchPhase && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(touchIndex).fingerId);
#else
            return Input.GetMouseButton(touchIndex) && !EventSystem.current.IsPointerOverGameObject();
#endif
        }
    }
}
