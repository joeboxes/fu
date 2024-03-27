using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRInterface : MonoBehaviour
{
    public delegate void ControllerEvent();
    // delegate void ControllerButtonADownEvent;

    public static string EventNameLeftButtonA = "EventNameLeftButtonA";
    public static string EventNameLeftButtonB = "EventNameLeftButtonB";
    public static string EventNameRightButtonA = "EventNameRightButtonA";
    public static string EventNameRightButtonB = "EventNameRightButtonB";
    
    public const string EventNameLongPressLeftButtonA = "EventNameLongPressLeftButtonA";
    public const string EventNameLongPressLeftButtonB = "EventNameLongPressLeftButtonB";
    public const string EventNameLongPressRightButtonA = "EventNameLongPressRightButtonA";
    public const string EventNameLongPressRightButtonB = "EventNameLongPressRightButtonB";
    
    public const string EventNameQuickTapRightButtonA = "EventNameQuickTapRightButtonA";
    
    private float rightButtonAStart = 0.0f;
    private float rightButtonAEnd = 0.0f;
    private float rightButtonADuration = 0.0f;
    private int rightButtonATapCount = 0;
    
    public static float LongPressTimeSeconds = 1.0f; // 1-1.5
    public static float QuickTapTimeSeconds = 0.20f; // 0.10 - 0.50
    
    public Dispatch dispatch = new Dispatch();

    // public ControllerEvent ControllerEventDelegate;
    public ControllerEvent UpdateEventDelegate;

    private static XRInterface _instance;
    public static XRInterface Instance
    {
        get { return _instance; }
    }
    
    [SerializeField] private OVRCameraRig rig;
    [SerializeField] private GameObject rigTracking;
    [SerializeField] private OVRManager manager;

    private Coroutine coroutineLongPressLeftA;
    private Coroutine coroutineLongPressLeftB;
    
    private Coroutine coroutineLongPressRightA;
    private Coroutine coroutineLongPressRightB;

    void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 
    }

    // Update is called once per frame
    void Update()
    {
        var left = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.LeftHanded);
        var right = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.RightHanded);
        // var leftPosition = OVRInput.GetLocalControllerPosition(left);
        // var rightPosition = OVRInput.GetLocalControllerPosition(right);
        // var leftRotation = OVRInput.GetLocalControllerRotation(left);
        // var rightRotation = OVRInput.GetLocalControllerRotation(right);
        
        
        var leftA = OVRInput.Get(OVRInput.Button.One, left);
        if (leftA != leftButtonA)
        {
            if (leftA) // button press start
            {
                StartLongPressLeftA();
            }
            else // button press end
            {
                StopLongPressLeftA();
            }
            leftButtonA = leftA;
            dispatch.AlertAll(EventNameLeftButtonA, leftButtonA);
        }
        
        var leftB = OVRInput.Get(OVRInput.Button.Two, left);
        if (leftB != leftButtonB)
        {
            if (leftB) // button press start
            {
                StartLongPressLeftB();
            }
            else // button press end
            {
                StopLongPressLeftB();
            }
            leftButtonB = leftB;
            dispatch.AlertAll(EventNameLeftButtonB, leftButtonB);
        }
        
        var rightA = OVRInput.Get(OVRInput.Button.One, right);
        if (rightA != rightButtonA)
        {
            if (rightA) // button press start
            {
                StartLongPressRightA();
                rightButtonAStart = Time.time;
                // reset tap count when enough time has elapsed
                if (rightButtonAStart - rightButtonAEnd > QuickTapTimeSeconds)
                {
                    rightButtonATapCount = 0;
                }
            }
            else // button press end
            {
                StopLongPressRightA();
                rightButtonAEnd = Time.time;
                rightButtonADuration = rightButtonAEnd - rightButtonAStart;
                if (rightButtonADuration < QuickTapTimeSeconds)
                {
                    rightButtonATapCount += 1;
                    dispatch.AlertAll(EventNameQuickTapRightButtonA, rightButtonATapCount);
                }
                else // this may be unnecessary
                {
                    rightButtonATapCount = 0;
                }
            }
            rightButtonA = rightA;
            dispatch.AlertAll(EventNameRightButtonA, rightButtonA);
        }
        
        var rightB = OVRInput.Get(OVRInput.Button.Two, right);
        if (rightB != rightButtonB)
        {
            if (rightB) // button press start
            {
                StartLongPressRightB();
            }
            else // button press end
            {
                StopLongPressRightB();
            }
            rightButtonB = rightB;
            dispatch.AlertAll(EventNameRightButtonB, rightButtonB);
        }
        
        
        UpdateEventDelegate?.Invoke();
    }

    // LEFT A
    private void StartLongPressLeftA()
    {
        StopLongPressLeftA();
        coroutineLongPressLeftA = StartCoroutine(CoroutineLongPressLeftA());
    }
    private void StopLongPressLeftA()
    {
        if (coroutineLongPressLeftA != null)
        {
            StopCoroutine(coroutineLongPressLeftA);
        }
    }
    private IEnumerator CoroutineLongPressLeftA()
    {
        yield return new WaitForSeconds(LongPressTimeSeconds);
        if (coroutineLongPressLeftA != null)
        {
            dispatch.AlertAll(EventNameLongPressLeftButtonA, null);
            coroutineLongPressLeftA = null;
        }
    }
    
    // LEFT B
    private void StartLongPressLeftB()
    {
        StopLongPressLeftB();
        coroutineLongPressLeftB = StartCoroutine(CoroutineLongPressLeftB());
    }
    private void StopLongPressLeftB()
    {
        if (coroutineLongPressLeftB != null)
        {
            StopCoroutine(coroutineLongPressLeftB);
        }
    }
    private IEnumerator CoroutineLongPressLeftB()
    {
        yield return new WaitForSeconds(LongPressTimeSeconds);
        if (coroutineLongPressLeftB != null)
        {
            dispatch.AlertAll(EventNameLongPressLeftButtonB, null);
            coroutineLongPressLeftB = null;
        }
    }
    
    // RIGHT A
    private void StartLongPressRightA()
    {
        StopLongPressRightA();
        coroutineLongPressRightA = StartCoroutine(CoroutineLongPressRightA());
    }
    private void StopLongPressRightA()
    {
        if (coroutineLongPressRightA != null)
        {
            StopCoroutine(coroutineLongPressRightA);
        }
    }
    private IEnumerator CoroutineLongPressRightA()
    {
        yield return new WaitForSeconds(LongPressTimeSeconds);
        if (coroutineLongPressRightA != null)
        {
            dispatch.AlertAll(EventNameLongPressRightButtonA, null);
            coroutineLongPressRightA = null;
        }
    }
    
    // RIGHT B
    private void StartLongPressRightB()
    {
        StopLongPressRightB();
        coroutineLongPressRightB = StartCoroutine(CoroutineLongPressRightB());
    }
    private void StopLongPressRightB()
    {
        if (coroutineLongPressRightB != null)
        {
            StopCoroutine(coroutineLongPressRightB);
        }
    }
    private IEnumerator CoroutineLongPressRightB()
    {
        yield return new WaitForSeconds(LongPressTimeSeconds);
        if (coroutineLongPressRightB != null)
        {
            dispatch.AlertAll(EventNameLongPressRightButtonB, null);
            coroutineLongPressRightB = null;
        }
    }



    public Quaternion leftRotation()
    {
        var left = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.LeftHanded);
        var leftRotation = OVRInput.GetLocalControllerRotation(left);
        //var rigRotation = rigTracking.transform.rotation;
        return leftRotation;// * rigRotation;
    }

    public Quaternion rightRotation()
    {
        var right = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.RightHanded);
        var rightRotation = OVRInput.GetLocalControllerRotation(right);
        //var rigRotation = rigTracking.transform.rotation;
        return rightRotation;// * rigRotation;
    }

    public Vector3 leftLocation()
    {
        var left = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.LeftHanded);
        var leftPosition = OVRInput.GetLocalControllerPosition(left);
        var rigParent = rigTracking.transform.position;
        return rigParent + leftPosition;
    }
    
    public Vector3 rightLocation()
    {
        var right = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.RightHanded);
        var rightPosition = OVRInput.GetLocalControllerPosition(right);
        var rigParent = rigTracking.transform.position;
        return rigParent + rightPosition;
    }

    public bool isLeftButtonADown()
    {
        return leftButtonA;
    }
    public bool isLeftButtonBDown()
    {
        return leftButtonB;
    }

    private bool leftButtonA = false;
    private bool leftButtonB = false;
    private bool rightButtonA = false;
    private bool rightButtonB = false;
}
