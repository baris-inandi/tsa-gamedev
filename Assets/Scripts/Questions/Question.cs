using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/QuestionScriptableObject", order = 1)]
public class Question : ScriptableObject
{
	public string question;
	public string Answer_A;
	public string Answer_B;
	public string Answer_C;
	public string Answer_D;

    [Range(1,4)]
	public byte RightAnswer = 1;

}
