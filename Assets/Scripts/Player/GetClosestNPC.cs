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

    [SerializeField] 
    private Transform[] nonPlayableCharacters;

    [HideInInspector] public Transform closestNonPlayableCharacter;
    
    private void Update()
    {
        GetClosestPlayer(nonPlayableCharacters);
    }

    Transform GetClosestPlayer (Transform[] players)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in players)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        
        closestNonPlayableCharacter = bestTarget;
        return bestTarget;
    }

}
