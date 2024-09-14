using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public partial class InputManager : MonoBehaviour
{
    public float MaxClickDistance;
    InputAction clickAction;
    InputAction mousePosition;

    Camera mainCamera;


    void Awake()
    {
        mainCamera = Camera.main;
        clickAction = InputSystem.actions.FindAction("Click");
        mousePosition = InputSystem.actions.FindAction("Point");
    }

    private void OnEnable()
    {
        clickAction.performed += OnClick;
        mousePosition.performed += OnMove;
    }

    private void OnDisable()
    {
        clickAction.performed -= OnClick;
        mousePosition.performed -= OnMove;
    }

    Vector2 movPos;
    private void OnMove(InputAction.CallbackContext context)
    {
        var mousePos = context.ReadValue<Vector2>();
        movPos = mousePos;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(movPos);
        InputRaycastResult result = GetClickablesFromRay(ray);

        if (result != null)
        {
            result.HoverAll();
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        var mousePos = mousePosition.ReadValue<Vector2>();

        //VR Input ray would come from the hand thing
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        InputRaycastResult result = GetClickablesFromRay(ray);

        if (result != null)
        {
            result.ClickAll();
        }
    }

    private InputRaycastResult GetClickablesFromRay(Ray ray)
    {

        if (Physics.Raycast(ray, out RaycastHit hit, MaxClickDistance))
        {
            IClickable3D[] clickables = hit.collider.GetComponents<IClickable3D>();

            if (clickables.Length == 0)
                return null;

            return new InputRaycastResult(hit, clickables);
        }

        return null;
    }
}
