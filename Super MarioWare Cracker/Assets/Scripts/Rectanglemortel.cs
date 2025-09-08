using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;
public class Rectanglemortel : MonoBehaviour


{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rectanglemortel"))
        {
            Debug.Log("GROSLOSER") ;
            StartCoroutine(ResetGame());
        }

        IEnumerator ResetGame()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
