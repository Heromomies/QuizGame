using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int numberOfMovement;
    public Slider sliderMovement;
    public LayerMask layerMask;
    private void Start()
    {
        sliderMovement.value = numberOfMovement;
    }

    private void Update()
    {
        Death();
        
        if (numberOfMovement <= 0)
        {
            Movement(false);
        }
        else
        {
            Movement(true);
        }
        
        if (numberOfMovement > sliderMovement.value)
        {
            numberOfMovement = (int) sliderMovement.value;
        }
    }

    void Death()
    {
        if (numberOfMovement <= 0 && GetComponentInChildren<Game>().numberOfBottle == 0)
        {
            Debug.Log("I'm dead");
        }
    }
    // Update is called once per frame
    void Movement(bool canMove)
    {
        if(canMove)
        {
            RaycastHit hit;

            if (Input.GetKeyDown(KeyCode.Z) && !Physics.Raycast(transform.position, transform.forward, out hit, 1, layerMask))
            {
                transform.position += Vector3.forward;
                numberOfMovement--;
                UpdateSlider();
            }
            else if (Input.GetKeyDown(KeyCode.D) && !Physics.Raycast(transform.position, transform.right, out hit, 1, layerMask))
            {
                transform.position += Vector3.right;
                numberOfMovement--;
                UpdateSlider();
            }
            else if (Input.GetKeyDown(KeyCode.Q) && !Physics.Raycast(transform.position, -transform.right, out hit, 1, layerMask))
            {
                transform.position += Vector3.left;
                numberOfMovement--;
                UpdateSlider();
            }
            else if (Input.GetKeyDown(KeyCode.S) && !Physics.Raycast(transform.position, -transform.forward, out hit, 1, layerMask))
            {
                transform.position -= Vector3.forward;
                numberOfMovement--;
                UpdateSlider();
            }
        }
    }

    public void UpdateSlider()
    {
        sliderMovement.value = numberOfMovement;
    }
}
