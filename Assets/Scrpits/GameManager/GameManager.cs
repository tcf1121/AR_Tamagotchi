using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public static AppTimeTracker appTimeTracker;
    public static float LastTime;
    private void Awake()
    {
        SetSingleton();
        appTimeTracker = GetComponent<AppTimeTracker>();
    }
    private void SetSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {

    }

    private void GameOver()
    {

    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
    }

    private void Update()
    {

    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
