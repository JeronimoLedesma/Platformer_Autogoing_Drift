using System.Collections;
using TMPro;
using UnityEngine;

public class FakeGoal : MonoBehaviour, IObjective
{
    [SerializeField] GameObject nextGoal;
    [SerializeField] TextMeshProUGUI reverseText;
    [SerializeField] float timeStop;

    public void ObjectiveReached()
    {
        StartCoroutine(StopTime());   
        reverseText.text = "REVERSE! TO THE START!";
        nextGoal.SetActive(true);
    }

    IEnumerator StopTime()
    {
        GameManager.pause = true;
        yield return new WaitForSeconds(timeStop);
        GameManager.pause = false;
        Destroy(gameObject);
    }
}
