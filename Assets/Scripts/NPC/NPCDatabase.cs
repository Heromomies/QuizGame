using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCDatabase : MonoBehaviour
{
    [HideInInspector] public QuestionDatabase questionDatabase;
    [HideInInspector] public bool isVisited;

    private void Start()
    {
        questionDatabase = Database.instance.questionDatabase[Random.Range(0, Database.instance.questionDatabase.Count)];
        Database.instance.questionDatabase.Remove(questionDatabase);
    }
}
