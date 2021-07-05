using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioQuestion : QuestionUI
{
    [SerializeField]
    private TextMeshProUGUI questionStringText;
    [SerializeField] 
    private AudioClip questionAudio;
    [SerializeField] 
    private Button audioPlayButton;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void UpdateQuestionInfo(Question question)
    {
        questionStringText.text = question.questionText;    
        questionAudio = question.questionAudio;    
        base.UpdateQuestionInfo(question);
        audioPlayButton.onClick.AddListener(PlayAudio);
    }

    public void PlayAudio()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(questionAudio);
    }
}
