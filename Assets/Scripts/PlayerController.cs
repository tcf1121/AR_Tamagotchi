using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PetController _pet;
    [SerializeField] private LayerMask _dogInteractionLayer;
    [SerializeField] private Transform _movePoint;

    [Header("Food Config")]
    [SerializeField] private FoodController _foodPrefab;
    [SerializeField] private Vector3 _foodPositionOffset;
    [SerializeField] private Button _foodButton;

    [Header("Touch Inputs")]
    [SerializeField] private InputActionReference _touch;
    [SerializeField] private InputActionReference _touchPosition;

    [Header("Plane Alignment")]
    [SerializeField] private PlaneAlignment _dogMovePlaneType;
    [SerializeField] private PlaneAlignment _foodPlaneType;

    private RayInteractor _rayInteractor;
    private bool _isPressTouch;
    private Vector2 _touchPos;

    private bool _isActivateFood;
    private bool _canTouchDog => _isPressTouch && _pet.IsStateIdle;
    private bool _canMoveDog => _pet.IsStateIdle;
    private bool _canGiveFood => _isPressTouch && _isActivateFood;


    private void Awake() => Init();
    private void OnEnable() => EnableEvents();
    private void Update() => HandleControl();
    private void OnDisable() => DisableEvents();
    private void OnDestroy() => UnsubscribeEvents();

    private void HandleControl()
    {
        if (_canTouchDog) TouchDog();
        if (_canMoveDog) SetMovePoint();
        if (_canGiveFood) GiveFood();
    }

    private void TouchDog()
    {
        _rayInteractor
            .RayShot<InteractionTrigger>(_touchPos, _dogInteractionLayer)?
            .Counting();
    }

    private void GiveFood()
    {
        Pose horizontalPlane = _rayInteractor.GetPlaneFromTouchPosition(
            _touchPos,
            PlaneAlignment.HorizontalUp
        );

        if (horizontalPlane == default) return;

        _pet.Food = Instantiate(
            _foodPrefab,
            horizontalPlane.position + _foodPositionOffset,
            Quaternion.identity
        );

        _isActivateFood = false;
    }

    private void SetMovePoint()
    {
        Pose horizontalPlane = _rayInteractor.GetPlaneFromARCamera(_dogMovePlaneType);

        if (horizontalPlane == default) return;

        if (_pet.MoveTransform == null)
        {
            _pet.MoveTransform = _movePoint;
        }

        _movePoint.position = horizontalPlane.position;
    }

    private void ActivateFood()
    {
        _isActivateFood = true;
    }

    private void Init()
    {
        _rayInteractor = GetComponent<RayInteractor>();
        SubscribeEvents();
        Debug.Log(GameManager.CurrentPet);
        GameObject CurPet = Instantiate(GameManager.CurrentPet);
        _pet = CurPet.GetComponent<PetController>();
    }

    private void EnableEvents()
    {
        _touch.action.Enable();
        _touchPosition.action.Enable();

        _foodButton.onClick.AddListener(ActivateFood);
    }

    private void DisableEvents()
    {
        _touch.action.Disable();
        _touchPosition.action.Disable();

        _foodButton.onClick.RemoveListener(ActivateFood);
    }

    private void SubscribeEvents()
    {
        _touch.action.started += OnTouchStart;
        _touch.action.canceled += OnTouchCancel;
        _touchPosition.action.performed += OnTouchStay;
    }

    private void UnsubscribeEvents()
    {
        _touch.action.started -= OnTouchStart;
        _touch.action.canceled -= OnTouchCancel;
        _touchPosition.action.performed -= OnTouchStay;
    }

    private void OnTouchStart(InputAction.CallbackContext ctx)
    {
        _isPressTouch = true;
    }
    private void OnTouchCancel(InputAction.CallbackContext ctx)
    {
        _isPressTouch = false;
    }
    private void OnTouchStay(InputAction.CallbackContext ctx)
    {
        _touchPos = ctx.ReadValue<Vector2>();
    }
}
