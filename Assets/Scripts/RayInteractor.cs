using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RayInteractor : MonoBehaviour
{
    [SerializeField] private float _rayMaxDist = 10;

    private Camera _mainCamera;
    private ARRaycastManager _arRayManager;
    private List<ARRaycastHit> _arHits = new();

    private void Awake() => Init();

    private void Init()
    {
        _mainCamera = Camera.main;
        _arRayManager = _mainCamera.GetComponentInParent<ARRaycastManager>();
    }

    public T RayShot<T>(Vector2 touchPosition, LayerMask targetLayer) where T : Component
    {
        T result = null;

        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayMaxDist, targetLayer))
        {
            result = ComponentRegistry.GetAs<T>(hit.transform.gameObject);
        }

        return result;
    }

    public Pose GetPlaneFromARCamera(PlaneAlignment alignment)
    {
        Pose result = default;

        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);

        if (_arRayManager.Raycast(ray, _arHits, TrackableType.Planes))
        {
            ARRaycastHit hit;

            hit = _arHits.Find(h => (h.trackable as ARPlane).alignment == alignment);

            if (hit != default) result = hit.pose;
        }

        return result;
    }

    public Pose GetPlaneFromTouchPosition(Vector2 touchPosition, PlaneAlignment alignment)
    { 
        Pose result = default;

        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

        if (_arRayManager.Raycast(ray, _arHits, TrackableType.Planes))
        {
            ARRaycastHit hit;

            hit = _arHits.Find(h => (h.trackable as ARPlane).alignment == alignment);

            if (hit != default) result = hit.pose;
        }

        return result;
    }
}
