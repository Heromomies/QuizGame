using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextQuestion : QuestionUI
{
    [SerializeField]
    private TextMeshProUGUI questionStringText;
    
    public override void UpdateQuestionInfo(Question question)
    {
        questionStringText.text = question.questionText;    
        base.UpdateQuestionInfo(question);
    }
}
