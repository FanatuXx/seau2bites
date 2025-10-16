using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}


