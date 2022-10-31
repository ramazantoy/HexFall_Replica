using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public delegate void TouchEventHandler(TouchType touchType);

    public event TouchEventHandler TouchEvent;

    public delegate void RotateEventHandler();

    public event RotateEventHandler RotateEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    bool isPressing = false;

    public bool IsPressing
    {
        get
        {
            return isPressing;
        }
    }


    bool wasTouch = false;
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (Input.touchCount > 0)
            {
                wasTouch = true;
            }
            isPressing = true;
            PressDown();
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            if (Input.touchCount > 0)
            {
                wasTouch = true;
            }
            isPressing = true;
            HoldPressDown();
        }
        if (Input.GetMouseButtonUp(0) || (isPressing && Input.touchCount == 0 && wasTouch))
        {
            wasTouch = false;
            isPressing = false;
            PressUp();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }
#endif
#if !UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            isPressing = true;
            PressDown();
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            isPressing = true;
            HoldPressDown();
        }
        if (Input.GetMouseButtonUp(0) || (isPressing && Input.touchCount == 0))
        {
            isPressing = false;
            PressUp();
        }
#endif
    }

    Vector3 movementFromMainStartPos = new Vector3(0, 0, 0);


    public Vector3 MovementFromMainStartPos
    {
        get
        {
            return movementFromMainStartPos;
        }
    }
    
    private  void PressDown()
    {
        TouchEvent?.Invoke(TouchType.PressDown);
        
    } 
    private void HoldPressDown()
    {
        TouchEvent?.Invoke(TouchType.HoldPressDown);
    }
   private  void PressUp()
    {
        TouchEvent?.Invoke(TouchType.PressUp);
    }



    void Rotate()
    {
        RotateEvent?.Invoke();
    }

    private void OnDestroy()
    {
        CleanEvents();
    }

    private void CleanEvents()
    {
        TouchEvent = delegate { };
    }
    public Vector3 MousePosition
    {
        get
        {
            if (!isPressing)
            {
                return Vector3.zero;
            }
            else
            {
                if (Input.touchCount == 0)
                {
                    return Input.mousePosition;
                }
                else
                {
                    return Input.touches[0].position;
                }
            }
        }
    }
    public RaycastHit Raycast(Camera camera, LayerMask rayMask)
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(MousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask);
        return hit;
    }

}

public enum TouchType
{
    None = -1,
    PressDown = 0,
    HoldPressDown = 1,
    PressUp = 2
}