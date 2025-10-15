using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class AlarmDead : MonoBehaviour
{
    bool _checking = false;
    CharacterController _cc;
    VideoPlayer _vp;
    //GameObject _pawner;
    //GameObject _player;
    //Collider2D _hide;
    Renderer _renderer;



    private void Start()
    {
        //_pawner = GameObject.FindWithTag("ts");
        //_pawner = GameObject.FindWithTag("ts").GetComponent<GameObject>();
        //var videoPlayer = GetComponent<VideoPlayer>();
        //_player = GetComponent<GameObject>();
        //_hide = GetComponent<Collider2D>();
        //_hide = GameObject.FindWithTag("Cachette").GetComponent<Collider2D>();
        //_hide.enabled = false;
        //videoPlayer.enabled = true;
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    
    void Update()
    {
        //_pawner = GameObject.FindWithTag("ts")//.GetComponent<GameObject>();
        if (_checking)
        {
            _cc = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
            _vp = GameObject.FindWithTag("vp").GetComponent<VideoPlayer>();
            //_pawner = GameObject.FindWithTag("ts");
            //Collider2D collider2D1 = GameObject.FindWithTag("Player").GetComponentInChildren<Collider2D>();
            //_player = collider2D1;

            //_hide = GameObject.FindWithTag("Cachette").GetComponent<Collider2D>();
            //_hide.enabled = false;
            if (_cc.canbeKilled = true && !_cc.isHidden)
            {
                
                //Destroy(_pawner);
                //var videoPlayer = GetComponent<VideoPlayer>(); 
                //_vp.Play();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                _cc.canbeKilled = false;
                StartCoroutine(ResetGame());

            }

         

            IEnumerator ResetGame()
            {

                //videoPlayer.Play();
                //_player.SetActive(false);
                //Destroy(_pawner);
                _vp.Play();
                //Destroy(_pawner.gameObject);
                //_cc.isHidden = true;
                yield return new WaitForSeconds(10f);
                //_cc.isHidden = true;
                //_vp.enabled = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);


            }

           
            if (_renderer.isVisible)
            {
                Debug.Log("Object is visible");
            }
            else 
            {
                Debug.Log("Object is no longer visible");
            }

            
           
            //IEnumerator ResetGame()
            //{

            //    //videoPlayer.Play();
            //    _vp.Play();
            //    _cc.isHidden = true;    
            //    yield return new WaitForSeconds(10f);
            //    //_cc.isHidden = true;
            //    _vp.enabled = false;
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);


            //}

        }

     

        
    }

    //void OnBecameInvisible(SpriteRenderer _renderer)
    //{
    //    Debug.Log("caché");
    //    _cc.isHidden = true;
    //}

    //IEnumerator ResetGame()
    //{

    //    //videoPlayer.Play();
    //    //_player.SetActive(false);
    //    //Destroy(_pawner);
    //    _vp.Play();
    //    Destroy(_pawner.gameObject);
    //    //_cc.isHidden = true;
    //    yield return new WaitForSeconds(10f);
    //    //_cc.isHidden = true;
    //    //_vp.enabled = false;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    //}
    //IEnumerator ResetGame()
    //{

    //    //videoPlayer.Play();
    //    _vp.Play();
    //    //_hide.enabled = true;
    //    //yield return new WaitForSeconds(10f);

    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    //}
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
        yield return new WaitForSeconds(60f);
        _checking = false;
        Destroy(gameObject);
    }

    //    IEnumerator ResetGame()
    //    {

    //        videoPlayer.Play();
    //        yield return new WaitForSeconds(10f);
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    //    }
}
