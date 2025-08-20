using UnityEngine;
using System.Collections;

public class Lyrica : MonoBehaviour
{

    public float m_multiplier = 2f;
    public float m_duration = 5f;
    private float _tempSpeed;
 

    private void Awake()
    {
        _tempSpeed = gameObject.GetComponent<CharacterController>().speed;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Lyrica"))
        {
           
            gameObject.GetComponent<CharacterController>().speed *= m_multiplier;
            gameObject.GetComponent<CharacterController>().speed = Mathf.Clamp(gameObject.GetComponent<CharacterController>().speed, _tempSpeed, _tempSpeed * m_multiplier);
            StartCoroutine(ResetSpeed());
            other.gameObject.SetActive(false);
        }
    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(m_duration);
        gameObject.GetComponent<CharacterController>().speed = _tempSpeed;
    }

}
