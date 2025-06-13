using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;

public class GpsManager : MonoBehaviour
{
    [SerializeField] private float _refreshCycle = 1;
    [SerializeField] private int _maxRefreshWait = 20;

    public EarthLocation CurrentLocation { get; private set; }
    public UnityEvent<EarthLocation> OnLocationUpdated = new();
    
    private WaitForSeconds _cycle;
    private Coroutine _routine;

    public int RefreshCount { get; private set; }
    
    private void Awake() => Init();
    private void OnEnable() => StartGps();
    private void OnDisable() => StopGps();

    private void Init()
    {
        _cycle = new WaitForSeconds(_refreshCycle);
    }

    private void StartGps()
    {
        if(_routine != null) StopCoroutine(_routine);

        // Input.location.Start();
        _routine = StartCoroutine(GpsLoop());
    }

    private void StopGps()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }

        if (Input.location.status != LocationServiceStatus.Stopped)
        {
            Input.location.Stop();
        }
    }

    private IEnumerator GpsLoop()
    {
        // 지금이 몇번 갱신됐는지 있으면 좋을 것 같음.
        RefreshCount = 0;
        
        // GPS가 활성화 되어있는가? 되어있지 않으면 멈춰야 함.
        bool isGpsActive =
            !Input.location.isEnabledByUser ||
            !Permission.HasUserAuthorizedPermission(Permission.FineLocation);

        if (isGpsActive)
        {
            yield break;
        }

        // 서비스 시작
        Input.location.Start();
        
        // GPS가 현재 구동될 조건이 안 갖춰졌고 + 대기 카운트가 0 초과라면
        // 사이클 주기로 카운트를 차감, 반복하며 대기
        int count = _maxRefreshWait;
        while (Input.location.status == LocationServiceStatus.Initializing && count > 0)
        {
            count--;
            yield return _cycle;
        }

        // 카운트가 0 이하거나(카운트 다 됨) GPS 상태가 실패일 경우.
        // 멈춘다.
        if (count <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }

        // 이젠 정상구동 되었으니 돌리자.
        while (true)
        {
            LocationInfo data = Input.location.lastData;
            CurrentLocation = new EarthLocation(data.latitude, data.longitude);
            
            RefreshCount++;
            OnLocationUpdated?.Invoke(CurrentLocation);

            yield return _cycle;
        }
    }
}
