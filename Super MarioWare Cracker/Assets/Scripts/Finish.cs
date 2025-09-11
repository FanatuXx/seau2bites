using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Finish : MonoBehaviour

{
    public Animator animator;
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("BIEN OUEJ");
            GameManager.instance.NextLevel();
           

        }
    }

  
    
}

