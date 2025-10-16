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
    //public AudioSource audioSource;
    //public GameObject VideoMort;

    private void Start()
    {
        //VideoMort.SetActive(false);
        var videoPlayer = GetComponent<VideoPlayer>();
        //var audioSource = GetComponent<AudioSource>();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rectanglemortel"))
        {
            Debug.Log("GROSLOSER") ;
            //VideoMort.SetActive(true);

            //audioSource.enabled = false ;
            
            videoPlayer.Play();

            StartCoroutine(ResetGame());
        }

        IEnumerator ResetGame()
        {
            //VideoPlayer.Play();
           
            yield return new WaitForSeconds(5f);
            //audioSource.enabled = true ;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
