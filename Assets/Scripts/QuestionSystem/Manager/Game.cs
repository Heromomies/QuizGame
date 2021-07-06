using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [HideInInspector] public QuestionDatabase questionDatabase;
    [SerializeField]
    private Transform questionPanel;
    [SerializeField]
    private Transform answerPanel;
    [SerializeField]
    private Transform questionScreen;
    [SerializeField]
    private float detectionRangeMax = 2f;

    private QuestionSet _currentQuestionSet;
    private Question _currentQuestion;
    private int _currentQuestionIndex;
    private int _correctAnswers;
    private int _inputIndex;

    private Transform _closestNpc;

    void LoadQuestionSet()
    {
        _currentQuestionSet = questionDatabase.GetQuestionSet();
        _currentQuestion = _currentQuestionSet.questions[0]; 
    }

    void ClearAnswers()
    {
        foreach (Transform buttons in answerPanel)
        {
            Destroy(buttons.gameObject);
        }
    }

    private void Update()
    {
        _closestNpc = GetClosestNPC.instance.closestNonPlayableCharacter;
       
        float dist = Vector3.Distance(transform.position, _closestNpc.position);
        if (dist <= detectionRangeMax)
        {
            questionDatabase = _closestNpc.GetComponent<NPCDatabase>().questionDatabase;
            if (Input.GetKeyDown(KeyCode.E) && !_closestNpc.GetComponent<NPCDatabase>().isVisited)
            {
                questionScreen.gameObject.SetActive(true);
                LoadQuestionSet();
                UseQuestionTemplate(_currentQuestion.questionType);
                _closestNpc.GetComponent<NPCDatabase>().isVisited = true;
            }
        }
    }

    void UseQuestionTemplate(Question.QuestionType questionType)
    {
        for (int i = 0; i < questionPanel.childCount; i++)
        {
            questionPanel.GetChild(i).gameObject.SetActive(i == (int)questionType);
            if (i == (int)questionType)
            {
                questionPanel.GetChild(i).gameObject.GetComponent<QuestionUI>().UpdateQuestionInfo(_currentQuestion);
            }
        }
    }
    
    /*public void NextQuestionSet()
    {
        if (0 < questionDatabase.questionSets.Length - 1)
        {
            _correctAnswers = 0;
            _currentQuestionIndex = 0;
            questionScreen.gameObject.SetActive(true);
            LoadQuestionSet();
            UseQuestionTemplate(_currentQuestion.questionType);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }*/

    void NextQuestion()
    {
        if (_currentQuestionIndex < _currentQuestionSet.questions.Count - 1)
        {
            _currentQuestionIndex++;
            _currentQuestion = _currentQuestionSet.questions[_currentQuestionIndex]; 
            UseQuestionTemplate(_currentQuestion.questionType);
        }
        else
        {
            questionScreen.gameObject.SetActive(false);
            _currentQuestionIndex = 0;
        }
    }
    
    public void CheckAnswer(string answer, Image image)
    {
        if (answer == _currentQuestion.correctAnswerKey)
        {
            _correctAnswers++;
            image.color = Color.green;
        }
        else
        { 
            image.color = Color.red;
        }
    }

    public void NextQuestionButton()
    {
        ClearAnswers();
        NextQuestion();
    }
}
