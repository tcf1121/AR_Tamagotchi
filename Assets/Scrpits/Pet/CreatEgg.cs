using System.Collections;
using System.Collections.Generic;
using Mapbox.Directions;
using UnityEngine;
using UnityEngine.UI;

public class CreatEgg : MonoBehaviour
{
    [SerializeField] private StepCounter _stepCounter;
    private int _totalCount;
    private int _currentCount;
    [SerializeField] private Text _countText;
    [SerializeField] private GameObject _eggPrefab;
    void Awake() => Init();

    private void Init()
    {
        _totalCount = 1000;
        _currentCount = 0;
        _stepCounter.ChangeCount += CountUp;
    }

    void Update()
    {
        _countText.text = $"{_totalCount - _currentCount}걸음";
        if (_currentCount >= _totalCount)
        {
            _currentCount -= _totalCount;
            GetEgg();
        }
    }

    private void CountUp()
    {
        _currentCount++;
    }

    private void GetEgg()
    {
        GameManager.bag.AddPet(_eggPrefab);
    }
}
