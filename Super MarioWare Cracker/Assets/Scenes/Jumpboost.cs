using UnityEngine;

public class Jumpboost : MonoBehaviour
{
    public float jumpboost = 500f;
    public float m_duration = 5f;
    private float tempjump;
    


    private void Awake()
    {
        jumpboost = gameObject.GetComponent<CharacterController>().jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jumpboost"))
        {

            gameObject.GetComponent<CharacterController>().jumpForce *= jumpboost;
            gameObject.GetComponent<CharacterController>().jumpForce = Mathf.Clamp(gameObject.GetComponent<CharacterController>().jumpForce, tempjump, tempjump * jumpboost);
            StartCoroutine(ResetJump());
            other.gameObject.SetActive(false);
        }
    }
    System.Collections.IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(m_duration);
        gameObject.GetComponent<CharacterController>().jumpForce = tempjump;
    }

}

