using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;
public class Rectanglemortel : MonoBehaviour

    
{
    public VideoPlayer videoPlayer;
    //public GameObject VideoMort;

    private void Start()
    {
        //VideoMort.SetActive(false);
        var videoPlayer = GetComponent<VideoPlayer>();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rectanglemortel"))
        {
            Debug.Log("GROSLOSER") ;
            //VideoMort.SetActive(true);
            
            videoPlayer.Play();

            StartCoroutine(ResetGame());
        }

        IEnumerator ResetGame()
        {
            //VideoPlayer.Play();
           
            yield return new WaitForSeconds(10f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
