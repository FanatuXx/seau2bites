using UnityEngine;

public class Jumpboost : MonoBehaviour
{
    public float jumpboost = 1.1f;
    //public float m_duration = 5f;
    //private float tempjump;
    


    //private void Awake()
    //{
    //    jumpboost = gameObject.GetComponent<CharacterController>().jumpForce;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jumpboost"))
        {

            gameObject.GetComponent<CharacterController>().jumpForce *= jumpboost;
            other.gameObject.SetActive(false);
        }
    }

}

