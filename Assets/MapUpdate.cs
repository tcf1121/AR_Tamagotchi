using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class MapUpdate : MonoBehaviour
{
    [SerializeField] private GpsManager _gpsManager;
    [SerializeField] private AbstractMap _map;

    private Vector2d latitudeLongitude;
    private int _zoom;

    void Awake() => Init();

    private void Init()
    {
        _zoom = 16;
        SetLatitudeLongitude();
        UpdateMap();
    }

    void Update()
    {
        SetLatitudeLongitude();
        UpdateMap();
    }

    private void SetLatitudeLongitude()
    {
        latitudeLongitude.x = _gpsManager.CurrentLocation.Lat;
        latitudeLongitude.y = _gpsManager.CurrentLocation.Lng;
    }

    private void UpdateMap()
    {
        _map.Initialize(latitudeLongitude, _zoom);
    }
}
