using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/Question", order = 1)]
public class QuestionDatabase : ScriptableObject
{
    public QuestionSet[] questionSets;

    public QuestionSet GetQuestionSet()
    {
        foreach (QuestionSet questionSet in questionSets)
        {
            return questionSet;
        }
        return new QuestionSet();
    }
}

[System.Serializable]
public struct QuestionSet
{
    public List<Question> questions;
}