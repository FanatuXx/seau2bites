using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using Unity.Cinemachine;

public class Speedboost : MonoBehaviour
{

    public float m_multiplier = 2f;
    public float m_duration = 5f;
    private float _tempSpeed;
    public Volume chromaticab;
 

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
            StartCoroutine(ResetSpeed());
            other.gameObject.SetActive(false);
        }
    }
    IEnumerator ResetSpeed(Collider2D other)
    {
        yield return new WaitForSeconds(m_duration);
        chromaticab.enabled = false;
        other.gameObject.SetActive(true);
        gameObject.GetComponent<CharacterController>().speed = _tempSpeed;
    }

}
