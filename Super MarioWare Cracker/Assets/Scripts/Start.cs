using UnityEngine;

public class Start : MonoBehaviour
{


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.instance.NextLevel();
        }


    }

}
