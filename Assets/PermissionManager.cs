using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using TMPro;

public class PermissionManager : MonoBehaviour
{
    private string targetPermission = Permission.FineLocation;

    public TextMeshProUGUI _grantText;
    public TextMeshProUGUI _deniedText;

    private void Start() => RequestWithCallbacks();

    public void RequestsWithCallbacks()
    {
        string[] permissions = new string[]
        {
            Permission.FineLocation,
            Permission.Camera
        };

        PermissionCallbacks callbacks = new();
        Permission.RequestUserPermissions(permissions, callbacks);

        callbacks.PermissionGranted += OnGranted;
        callbacks.PermissionDenied += OnDenied;
    }

    private void OnGranted(string permission) => _grantText.text += $", {permission}";
    private void OnDenied(string permission) => _deniedText.text += $", {permission}";

    public void RequestWithCallbacks()
    {
        PermissionCallbacks callbacks = new();
        Permission.RequestUserPermission(targetPermission, callbacks);

        callbacks.PermissionGranted += t =>
        {
            //_grantText.text = "Granted";
        };
        callbacks.PermissionDenied += t =>
        {
            _deniedText.text = "Denied";
        };
    }

    public void Request()
    {
        // 1회성 요청
        Permission.RequestUserPermission(targetPermission);
    }
}
