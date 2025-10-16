using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class Speedboost : MonoBehaviour
{

    public float m_multiplier = 2f;
    float m_duration = 4f;
    private float _tempSpeed;
    public Volume chromaticab;
    public AudioSource pill2;
 

    private void Awake()
    {
        _tempSpeed = gameObject.GetComponent<CharacterController>().speed;
        chromaticab.enabled = false;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Speedboost"))
        {
           
            gameObject.GetComponent<CharacterController>().speed *= m_multiplier;
            gameObject.GetComponent<CharacterController>().speed = Mathf.Clamp(gameObject.GetComponent<CharacterController>().speed, _tempSpeed, _tempSpeed * m_multiplier);
            chromaticab.enabled = true;
            pill2.Play();
            StartCoroutine(ResetSpeed());
            other.gameObject.SetActive(false);
        }
    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(m_duration);
        chromaticab.enabled = false;
        gameObject.GetComponent<CharacterController>().speed = _tempSpeed;
    }

}
