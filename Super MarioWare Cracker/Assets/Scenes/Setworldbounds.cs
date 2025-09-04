using UnityEngine;

public class Setworldbounds : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;
        Globals.WorldBounds = bounds;
    }

   
}
