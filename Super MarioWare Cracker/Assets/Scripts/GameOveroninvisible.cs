using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverOnInvisible : MonoBehaviour
{
    public void OnBecameInvisible()
    {
        Debug.Log("LOSER");
    }
}
