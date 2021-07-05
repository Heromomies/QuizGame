using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageCaptionQuestion : QuestionUI
{
    [SerializeField]
    private TextMeshProUGUI questionStringText;
    [SerializeField] 
    private Image questionImage;
    
    public override void UpdateQuestionInfo(Question question)
    {
        questionStringText.text = question.questionText;    
        questionImage.sprite = question.questionImage;    
        base.UpdateQuestionInfo(question);
    }
}
