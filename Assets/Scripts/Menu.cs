using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            canvas.SetActive(!canvas.active);
            Time.timeScale = canvas.active ? 0f : 1f;
        }
    }



    public void Quit()
    {
        Application.Quit();
    }
}
