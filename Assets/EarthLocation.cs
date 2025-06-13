using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EarthLocation
{
    public float Lat { get; set; } // 위도
    public float Lng { get; set; } // 경도

    public EarthLocation(float lat, float lng)
    {
        Lat = lat;
        Lng = lng;
    }
}
