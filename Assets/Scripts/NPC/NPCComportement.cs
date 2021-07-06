using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComportement : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<PlayerMovement>())
		{
			Debug.Log("Je passe par là");
			other.transform.position -= Vector3.forward;
		}
	}
}
