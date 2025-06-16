using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveManager : MonoBehaviour
{
    public void GetOut()
    {
        SceneManager.LoadScene(1);
    }
}
