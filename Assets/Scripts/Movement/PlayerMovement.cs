using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int numberOfMovement;
    public Slider sliderMovement;

    private void Start()
    {
        sliderMovement.value = numberOfMovement;
    }

    private void Update()
    {
        Movement();
        Death();
    }

    void Death()
    {
        if (numberOfMovement <= 0)
        {
            Debug.Log("Restart Done");
        }
    }
    // Update is called once per frame
    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= Vector3.forward;
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S))
        {
            numberOfMovement--;
            UpdateSlider();
        }
    }

    void UpdateSlider()
    {
        sliderMovement.value = numberOfMovement;
    }
}
