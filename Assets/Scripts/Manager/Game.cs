using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public QuestionDatabase questionDatabase;
    [SerializeField]
    private Transform questionPanel;
    [SerializeField]
    private Transform answerPanel;
    [SerializeField]
    private Transform scoreScreen, questionScreen;
    [SerializeField]
    private TextMeshProUGUI scoreStats, scorePercentage;
    
    private QuestionSet _currentQuestionSet;
    private Question _currentQuestion;
    private int _level;
    private int _currentQuestionIndex;
    private int _correctAnswers;
    // Start is called before the first frame update
    void Start()
    {
        _level = PlayerPrefs.GetInt("level", 0);
        _level = 0;
        LoadQuestionSet();
        UseQuestionTemplate(_currentQuestion.questionType);
    }

    void LoadQuestionSet()
    {
        _currentQuestionSet = questionDatabase.GetQuestionSet(_level);
        _currentQuestion = _currentQuestionSet.questions[0]; 
    }

    void ClearAnswers()
    {
        foreach (Transform buttons in answerPanel)
        {
            Destroy(buttons.gameObject);
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
    
    public void NextQuestionSet()
    {
        if (_level < questionDatabase.questionSets.Length - 1)
        {
            _correctAnswers = 0;
            _currentQuestionIndex = 0;
            _level++;
            PlayerPrefs.SetInt("level", _level);
            scoreScreen.gameObject.SetActive(false);
            questionScreen.gameObject.SetActive(true);
            LoadQuestionSet();
            UseQuestionTemplate(_currentQuestion.questionType);
        }
        else
        {
            // load start menu
        }
    }

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
            scoreScreen.gameObject.SetActive(true);
            questionScreen.gameObject.SetActive(false);
            scorePercentage.text = $"Score : \n {(float) _correctAnswers / (float) _currentQuestionSet.questions.Count * 100}%";
            scoreStats.text = $"Questions: {_currentQuestionSet.questions.Count}\nCorrect: {_correctAnswers}";
        }
    }
    
    public void CheckAnswer(string answer)
    {
        if (answer == _currentQuestion.correctAnswerKey)
        {
            _correctAnswers++;
        }
        
        ClearAnswers();
        NextQuestion();
    }
}
