using UnityEngine;
using System.Collections;


/*
public class Jumpboost : MonoBehaviour
{
    public float jumpMultiplier = 1.5f;
    public float boostDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                StartCoroutine(ApplyJumpBoost(controller));
                gameObject.SetActive(false); // Désactive le consommable
            }
        }
    }

    private System.Collections.IEnumerator ApplyJumpBoost(CharacterController controller)
    {
        float originalJumpForce = controller.jumpForce;
        controller.jumpForce *= jumpMultiplier;

        yield return new WaitForSeconds(boostDuration);

        controller.jumpForce = originalJumpForce;
    }
}
*/

public class JumpBoostConsommable : MonoBehaviour
{

    public float jumpMultiplier = 1.5f;
    public float boostDuration = 5f;
    private float _tempJump;

    private void Awake()
    {
        _tempJump = gameObject.GetComponent<CharacterController>().jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jumpboost"))
        {

            gameObject.GetComponent<CharacterController>().jumpForce *= boostDuration;
            gameObject.GetComponent<CharacterController>().jumpForce = Mathf.Clamp(gameObject.GetComponent<CharacterController>().jumpForce, _tempJump, _tempJump * jumpMultiplier);
            StartCoroutine(ResetJump());
            other.gameObject.SetActive(false);
        }
    }
    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(boostDuration);
        gameObject.GetComponent<CharacterController>().jumpForce = _tempJump;
    }

}
