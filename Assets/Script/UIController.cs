using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public bool SelectStart = true;
    public bool SelectQuit = false;

    public bool isBegin = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (SelectStart && !SelectQuit)
            {
                SelectStart = false;
                SelectQuit = true;
            }
            else if (!SelectStart && SelectQuit)
            {
                SelectStart = true;
                SelectQuit = false;
            }
        }

        if (this.name == "Start" && SelectStart && !SelectQuit)
        {
            this.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
            GameObject.Find("Quit").GetComponent<Text>().fontStyle = FontStyle.Normal;
        }
        if (this.name == "Quit" && !SelectStart && SelectQuit)
        {
            this.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
            GameObject.Find("Start").GetComponent<Text>().fontStyle = FontStyle.Normal;

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!SelectQuit && SelectStart)
            {
                SceneManager.LoadScene(1);
            }
            else if (SelectQuit && !SelectStart)
            {
                Application.Quit();
            }
        }
    }
}
