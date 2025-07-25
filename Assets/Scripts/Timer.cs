using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    
    void Update()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = remainingTime.ToString("0.00");

        if (remainingTime <= 0)
        {
            Debug.Log("Perdiste");
            SceneManager.LoadScene(7);
        }
    }
}
