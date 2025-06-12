using UnityEngine;
using System;
using UnityEngine.UI;

public class AppTimeTracker : MonoBehaviour
{
    private const string QuitTimeKey = "LastQuitTime";
    public Text text;
    void Start()
    {
        if (PlayerPrefs.HasKey(QuitTimeKey))
        {
            string savedTimeStr = PlayerPrefs.GetString(QuitTimeKey);
            DateTime savedTime = DateTime.Parse(savedTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind);
            TimeSpan timePassed = DateTime.UtcNow - savedTime;
            text.text = $"앱 종료 후 경과 시간: {timePassed.TotalSeconds}초";
            GameManager.LastTime = (float)timePassed.TotalSeconds;
        }
        else
        {
            text.text = "처음 실행이거나 이전 기록 없음";
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveQuitTime();
        }
    }

    void OnApplicationQuit()
    {
        SaveQuitTime();
    }

    private void SaveQuitTime()
    {
        string now = DateTime.UtcNow.ToString("o"); // ISO 8601 형식
        PlayerPrefs.SetString(QuitTimeKey, now);
        PlayerPrefs.Save();
        text.text = $"앱 종료 시간 저장됨: {now}";
    }
}
