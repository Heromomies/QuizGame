using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private Game _game;
    private string _answer;
    
    // Start is called before the first frame update
    void Start()
    {
        _game = FindObjectOfType<Game>();
        GetComponent<Button>().onClick.AddListener(() => _game.CheckAnswer(_answer, gameObject.GetComponent<Image>()));   
    }
    
    public void SetAnswerButton(string answer)
    {
        _answer = answer;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = answer;
    }
}
