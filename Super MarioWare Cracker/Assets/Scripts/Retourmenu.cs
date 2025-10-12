using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RetourMenu : MonoBehaviour
{

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||  Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            SceneManager.LoadScene("Menu");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("25"))
        {
            SceneManager.LoadScene("Menu");

        }

    }
}
