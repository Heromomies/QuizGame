using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/Question", order = 1)]
public class QuestionDatabase : ScriptableObject
{
    public QuestionSet[] questionSets;

    public QuestionSet GetQuestionSet(int level)
    {
        foreach (QuestionSet questionSet in questionSets)
        {
            if (questionSet.level == level)
            {
                return questionSet;
            }
        }
        return new QuestionSet();
    }
}

[System.Serializable]
public struct QuestionSet
{
    public int level;
    public List<Question> questions;
}