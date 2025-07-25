using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour, IObjective
{
    public void ObjectiveReached()
    {
        Debug.Log("Llegaste al Objetivo");
        SceneManager.LoadScene(6);
    }
}
