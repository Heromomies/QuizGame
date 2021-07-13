using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClosestNPC : MonoBehaviour
{
    #region Singleton
    public static GetClosestNPC instance;
    
    void Awake(){

        if (instance == null){

            instance = this;
            DontDestroyOnLoad(this.gameObject);
    
            //Rest of your Awake code
    
        } else {
            Destroy(this);
        }
    }
    #endregion
    
    private GameObject[] _nonPlayableCharacters;

    [HideInInspector] public Transform closestNonPlayableCharacter;

    private void Start()
    {
        _nonPlayableCharacters = GameObject.FindGameObjectsWithTag("NPC");
    }

    private void Update()
    {
        GetClosestPlayer(_nonPlayableCharacters);
    }
    
    Transform GetClosestPlayer (GameObject[] players)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in players)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        
        closestNonPlayableCharacter = bestTarget;
        return bestTarget;
    }

}
