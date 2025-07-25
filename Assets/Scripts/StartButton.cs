using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(5);
    }
}
