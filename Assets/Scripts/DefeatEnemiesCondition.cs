using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatEnemiesCondition : MonoBehaviour
{
    int numberOfEnemies = 13;

    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies == GameManager.defeatedEnemies)
        {
            SceneManager.LoadScene(6);
        }
    }
}
