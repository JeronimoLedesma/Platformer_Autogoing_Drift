using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int lastLevel;
    public static int defeatedEnemies;
    public static bool pause;

    private void Start()
    {
        pause = false;
    }
}
