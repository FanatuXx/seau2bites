
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class Groundcheck : MonoBehaviour
{
    public bool isGrounded;
    public void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }


    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {

            isGrounded = false;


        }


    }
}
