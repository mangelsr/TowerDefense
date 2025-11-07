using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private InputAction _touch;
    private InputAction _touchPosition;
    private Camera _mainCamera;

    public delegate void PlatformTouched(GameObject platform);

    public event PlatformTouched OnPlatformTouched;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void OnEnable()
    {
        TouchSimulation.Enable();
        inputActions.Enable();
        _touch = inputActions.FindAction("Touch");
        _touchPosition = inputActions.FindAction("TouchPosition");
        _touch.performed += Touch;
    }

    void OnDisable()
    {
        _touch.performed -= Touch;
        inputActions.Disable();
        TouchSimulation.Disable();
    }

    private void Touch(InputAction.CallbackContext obj)
    {
        Vector2 positionTouched = _touchPosition.ReadValue<Vector2>();
        Vector3 positionTouched3D = new Vector3(positionTouched.x, positionTouched.y, _mainCamera.farClipPlane);
        Ray screenRay = _mainCamera.ScreenPointToRay(positionTouched3D);

        Debug.Log($"The screen have been touched on position: {positionTouched}");

        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit, Mathf.Infinity))
        {
            GameObject gameObject = hit.transform.gameObject;
            Debug.Log($"Hit object: {gameObject.name}");

            if (gameObject.tag == "Platform")
            {
                Debug.Log("Platform Touched");
                if (OnPlatformTouched != null)
                {
                    OnPlatformTouched(gameObject);
                }
            }
            else
            {
                Debug.Log("Raycast didn't hit anything");
            }

        }
    }
}
