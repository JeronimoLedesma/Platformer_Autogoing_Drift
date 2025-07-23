using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    [SerializeField] int level;
     public void LoadLevel()
    {
        Debug.Log("Cargando nivel " +  level);
        /*SceneManager.LoadScene(level);*/
    }
}
