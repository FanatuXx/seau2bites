using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class suisseallemandscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        StartCoroutine(NextLevel());
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton8))
    //    {
    //        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}


    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(10f);
        GameManager.instance.NextLevel();

    }
}
