using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Properties;


public class Finish : MonoBehaviour
{

    public Animator animator; //anim
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("BIEN OUEJ");
            animator.SetBool("Finish", true);
            StartCoroutine(NextLevel());

        }

    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.NextLevel();
    }

  
    
}

