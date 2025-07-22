using UnityEngine;

public class Goal : MonoBehaviour, IObjective
{
    public void ObjectiveReached()
    {
        Debug.Log("Llegaste al Objetivo");
        //Logica de victoria
    }
}
