using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_location : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject Button;
    public GameObject button = GameObject.Find("ButtonName"); 
    void Start() {
        // Replace "ButtonName" with the name of your Button
        if (button != null)
        {
            Debug.Log("Button Position: " + button.transform.position);
        }
        else
        {
            Debug.LogWarning("Button not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(button.transform.position);
        
    }
}
