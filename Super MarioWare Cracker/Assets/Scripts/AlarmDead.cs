using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

public class AlarmDead : MonoBehaviour
{
    bool _checking = false;
    CharacterController _cc;
    public VideoPlayer videoPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var videoPlayer = GetComponent<VideoPlayer>();
    }
    private void Start()
    {
        //var videoPlayer = GetComponent<VideoPlayer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_checking)
        {
            _cc = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
            if (!_cc.isHidden)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
                videoPlayer.Play();
            }
        }
    }
    public void Launch()
    {
        StartCoroutine(AnimKill());
    }

    IEnumerator AnimKill()
    {
        yield return new WaitForSeconds(3f);
        _checking = true;
        
        yield return new WaitForSeconds(1f);
        _checking = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
