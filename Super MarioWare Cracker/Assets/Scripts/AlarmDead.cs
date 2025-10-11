using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

public class AlarmDead : MonoBehaviour
{
    bool _checking = false;
    CharacterController _cc;
    //public VideoPlayer videoPlayer;


   
    //private void Start()
    //{
    //    var videoPlayer = GetComponent<VideoPlayer>();
    //    videoPlayer.enabled = true;
    //}
   
    // Update is called once per frame
    void Update()
    {
        if (_checking)
        {
            _cc = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
            if (!_cc.isHidden)
            {

                //var videoPlayer = GetComponent<VideoPlayer>(); 
                //videoPlayer.Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //StartCoroutine(ResetGame());
            }

        }

     

        
    }
    public void Launch()
    {
        StartCoroutine(AnimKill());
    }
    

    IEnumerator AnimKill()
    {
        yield return new WaitForSeconds(4f);
        _checking = true;
        
        yield return new WaitForSeconds(1f);
        _checking = false;
        //yield return new WaitForSeconds(20f);
        //Destroy(gameObject);
    }

    //IEnumerator ResetGame()
    //{

    //    //videoPlayer.Play();
    //    yield return new WaitForSeconds(10f);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    //}
}
