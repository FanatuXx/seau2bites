using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Videointroscript : MonoBehaviour
{
    
    private void Awake()
    {
        StartCoroutine(NextLevel());  
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    private IEnumerator NextLevel ()
    {
        yield return new WaitForSeconds(124);
        GameManager.instance.NextLevel();

    }
}
