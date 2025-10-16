using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class Jumpboost : MonoBehaviour
{
    public float jumpboost = 1.1f;
    public Volume postex;
    public AudioSource needle;
    //public float m_duration = 5f;
    //private float tempjump;



    private void Start()
    {
        postex.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jumpboost"))
        {

            gameObject.GetComponent<CharacterController>().jumpForce *= jumpboost;
            other.gameObject.SetActive(false);
            postex.enabled = true;
            needle.Play();
            StartCoroutine(ResetJump(other));
        }

        IEnumerator ResetJump(Collider2D other)
        {
            yield return new WaitForSeconds(15);
            gameObject.GetComponent<CharacterController>().jumpForce = 8;
            
            postex.enabled = false;
            other.gameObject.SetActive(true);
        }

        
    }

}

