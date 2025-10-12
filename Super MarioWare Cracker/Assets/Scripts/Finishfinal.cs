
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


public class Finishfinal : MonoBehaviour
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
        if (other.gameObject.CompareTag("Finishfinal"))
        {
            Debug.Log("C FINI");
            videoPlayer.Play();
            StartCoroutine(NextLevel());

        }

    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(10);
        GameManager.instance.NextLevel();
    }



}