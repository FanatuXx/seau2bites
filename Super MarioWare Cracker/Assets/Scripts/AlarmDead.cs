using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmDead : MonoBehaviour
{
    bool _checking = false;
    CharacterController _cc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
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
