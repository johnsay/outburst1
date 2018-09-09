
using System;
using UnityEngine;

[Serializable]
public class QuestionPack
{
	public string Title;
	public Sprite Icon;
	public string Description;
	public Question[] Content;

}

[Serializable]
public class Question
{
	public string Description;

	public string[] Answers;

}
