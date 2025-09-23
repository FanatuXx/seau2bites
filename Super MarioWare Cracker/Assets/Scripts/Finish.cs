using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Properties;


public class Finish : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator; //anim
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("BIEN OUEJ");
            animator.SetBool("Finish", true);
            
            
            rb.gravityScale = 0.001f;
            StartCoroutine(NextLevel());
            //GetComponent<Rigidbody>().isKinematic = true;
            //rb.bodyType = RigidbodyType2D.Kinematic;
            

        }

        

    }
    IEnumerator NextLevel()
    {
        //yield return new WaitForSeconds(1);
        //rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(3);
        
        //rb.bodyType = RigidbodyType2D.Dynamic;
        //GetComponent<Rigidbody>().isKinematic = false;
        GameManager.instance.NextLevel();
    }

  
    
}

