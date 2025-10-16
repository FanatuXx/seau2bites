using UnityEngine;

public class Start : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            GameManager.instance.NextLevel();
        }


    }

}
