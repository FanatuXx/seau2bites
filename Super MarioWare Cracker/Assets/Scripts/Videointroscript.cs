using UnityEngine;
using System.Collections;

public class Videointroscript : MonoBehaviour
{
    
    private void Awake()
    {
        StartCoroutine(NextLevel());  
    }

    
    private IEnumerator NextLevel ()
    {
        yield return new WaitForSeconds(124);
        GameManager.instance.NextLevel();

    }
}
