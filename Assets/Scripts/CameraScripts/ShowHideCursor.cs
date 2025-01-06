using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCursor : MonoBehaviour
{
    private bool ShowHide = true;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && ShowHide){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            ShowHide = false;
        }

        else if (Input.GetKeyDown(KeyCode.B) && !ShowHide){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ShowHide = true;
        }
    }
}
