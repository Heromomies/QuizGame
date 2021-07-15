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
    [Header("Transform")]
    [SerializeField]
    private Transform questionPanel;
    [SerializeField]
    private Transform answerPanel;
    [SerializeField]
    private Transform questionScreen;
    [SerializeField]
    private Transform buttonSkip;
    [SerializeField]
    private Transform buttonBottle;
    
    [Space]
    [Header("Gameplay")]
    [SerializeField]
    private Slider sliderMovement;
    [SerializeField]
    private float detectionRangeMax;
    [SerializeField]
    private int numberOfMovementAdded;

    private QuestionSet _currentQuestionSet;
    private Question _currentQuestion;
    private int _currentQuestionIndex;
    [HideInInspector] public int numberOfBottle;
    [HideInInspector] public bool inQuestion;
    private int _inputIndex;

    private Transform _closestNonPlayableCharacter;
    private GameObject[] _nonPlayableCharacters;
    private void Start()
    {
        _nonPlayableCharacters = GameObject.FindGameObjectsWithTag("NPC");
        GetClosestPlayer(_nonPlayableCharacters);
    }

    void LoadQuestionSet()
    {
        _currentQuestionSet = questionDatabase.GetQuestionSet();
        _currentQuestion = _currentQuestionSet.questions[0]; 
    }

    void ClearAnswers()
    {
        foreach (Button buttons in answerPanel.GetComponentsInChildren<Button>())
        {
            Destroy(buttons.gameObject);
        }
        
        buttonSkip.gameObject.SetActive(false);
        sliderMovement.gameObject.SetActive(true);
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
        
        _closestNonPlayableCharacter = bestTarget;
        return bestTarget;
    }
    private void Update()
    {
        GetClosestPlayer(_nonPlayableCharacters);
        if (Input.GetKeyDown(KeyCode.A))
        {
            numberOfBottle++;
            ActualizeNumberOfBottle();
        }

        float dist = Vector3.Distance(transform.position, _closestNonPlayableCharacter.position);
        
        if (dist <= detectionRangeMax)
        {
            questionDatabase = _closestNonPlayableCharacter.GetComponent<NPCDatabase>().questionDatabase;
            if (Input.GetKeyDown(KeyCode.E) && !_closestNonPlayableCharacter.GetComponent<NPCDatabase>().isVisited)
            {
                questionScreen.gameObject.SetActive(true);
                LoadQuestionSet();
                UseQuestionTemplate(_currentQuestion.questionType);
                _closestNonPlayableCharacter.GetComponent<NPCDatabase>().isVisited = true;
                inQuestion = true;
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
    
    public void NextQuestionSet()
    {
        if (0 < questionDatabase.questionSets.Length - 1)
        {
            _currentQuestionIndex = 0;
            questionScreen.gameObject.SetActive(true);
            LoadQuestionSet();
            UseQuestionTemplate(_currentQuestion.questionType);
        }
        else
        {
            SceneManager.LoadScene(0);
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
            questionScreen.gameObject.SetActive(false);
            inQuestion = false;
            _currentQuestionIndex = 0;
        }
    }
    
    public void CheckAnswer(string answer, Image image)
    {
        if (answer == _currentQuestion.correctAnswerKey)
        {
            image.color = Color.green;
            numberOfBottle++;
            ActualizeNumberOfBottle();
            foreach (var button in answerPanel.GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }
        }
        else
        { 
            image.color = Color.red;
            foreach (var button in answerPanel.GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }
        }
        buttonSkip.gameObject.SetActive(true);
        sliderMovement.gameObject.SetActive(false);
    }

    public void NextQuestionButton()
    {
        ClearAnswers();
        NextQuestion();
    }

    public void OnUseBottle()
    {
        if(gameObject.GetComponentInParent<PlayerMovement>().numberOfMovement < 10 && numberOfBottle > 0)
        {
            gameObject.GetComponentInParent<PlayerMovement>().numberOfMovement += numberOfMovementAdded;
            gameObject.GetComponentInParent<PlayerMovement>().UpdateSlider();
            numberOfBottle--;
            ActualizeNumberOfBottle();
        }
    }

    public void ActualizeNumberOfBottle()
    {
        if (numberOfBottle < 0)
        {
            numberOfBottle = 0;
        }
        buttonBottle.GetComponentInChildren<TextMeshProUGUI>().text = $"{numberOfBottle}";
    }
}
