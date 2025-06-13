using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationChecker : MonoBehaviour
{
    // 이 부분은 어차피 UI로 데이터만 확인하는 용도라 빠르게 저 혼자 후딱 진행했어요
    [SerializeField] private GpsManager _gpsManager;

    [SerializeField] private TextMeshProUGUI _latText;
    [SerializeField] private TextMeshProUGUI _lngText;
    [SerializeField] private TextMeshProUGUI _countText;

    private void OnEnable() => SubscribeEvents();

    private void SubscribeEvents()
    {
        _gpsManager.OnLocationUpdated.AddListener(SetUI);
    }

    private void SetUI(EarthLocation loaction)
    {
        _latText.text = loaction.Lat.ToString();
        _lngText.text = loaction.Lng.ToString();
        _countText.text = _gpsManager.RefreshCount.ToString();
    }
}
