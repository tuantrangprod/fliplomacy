using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipeLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipeRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipeTop();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipeBottom();
        }
    }
    private void OnSwipeLeft() {
        Debug.Log("Left");
        //ShowText.text = "Left";
    }

    private void OnSwipeRight() {
        Debug.Log("Right");
        //ShowText.text = "Right";
        
    }

    private void OnSwipeTop() {
        Debug.Log("Top");
        //ShowText.text = "Top";
    }

    private void OnSwipeBottom() {
        Debug.Log("Down");
        //ShowText.text = "Botton";
    }
}
