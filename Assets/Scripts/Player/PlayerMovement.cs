using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int numberOfMovement;
    public Slider sliderMovement;
    public LayerMask layerMask;
    public Transform playerMesh;

    private bool _isTouching;
    private void Start()
    {
        sliderMovement.value = numberOfMovement;
    }

    private void Update()
    {
        Death();
        
        if (numberOfMovement <= 0 || GetComponentInChildren<Game>().inQuestion)
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
        if (numberOfMovement <= 0 && GetComponentInChildren<Game>().numberOfBottle == 0 && !_isTouching)
        {
            SceneManager.LoadScene(0);
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
                playerMesh.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                numberOfMovement--;
                _isTouching = false;
                
                UpdateSlider();
            }
            else if (Input.GetKeyDown(KeyCode.D) && !Physics.Raycast(transform.position, transform.right, out hit, 1, layerMask))
            {
                transform.position += Vector3.right;
                playerMesh.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                numberOfMovement--;
                _isTouching = false;
                
                UpdateSlider();
            }
            else if (Input.GetKeyDown(KeyCode.Q) && !Physics.Raycast(transform.position, -transform.right, out hit, 1, layerMask))
            {
                transform.position += Vector3.left;
                playerMesh.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                numberOfMovement--;
                _isTouching = false;
                
                UpdateSlider();
            }
            else if (Input.GetKeyDown(KeyCode.S) && !Physics.Raycast(transform.position, -transform.forward, out hit, 1, layerMask))
            {
                transform.position -= Vector3.forward;
                playerMesh.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                numberOfMovement--;
                _isTouching = false;    
                
                UpdateSlider();
            }
            else
            {
                _isTouching = true;
            }
        }
    }

    public void UpdateSlider()
    {
        sliderMovement.value = numberOfMovement;
    }
}
