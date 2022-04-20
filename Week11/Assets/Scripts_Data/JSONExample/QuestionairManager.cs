using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionairManager : MonoBehaviour
{
    [SerializeField]
    JSONLoader jsonLoader;
	List<int> questionList = new List<int>();
	int currentQuestionIndex;

	[SerializeField]
	Text tX_QuestionContent;
	[SerializeField]
	Text tX_QuestionResult;
	[SerializeField]
	Text tX_Score;
	int score = 0;

	[SerializeField]
	Text[] tX_Answers;
	[SerializeField]
	GameObject[] button_Answers;
	[SerializeField]
	GameObject button_NextQuestion;


	//Change answer orders
	string[] answerOrders = { "a", "b", "c", "d" };

	// Start is called before the first frame update
	void Awake()
    {
		jsonLoader.jsonRefreshed += JSONLoaded;
	}

	void JSONLoaded()
	{
		jsonLoader.jsonRefreshed -= JSONLoaded;
		//Questions are not repeatable
		questionList = new List<int>();
		for (int i = 0; i < jsonLoader.currentJSON.Count; i++)
		{
			//Create question list
			questionList.Add(i);
		}
		SelectNextQuestion();
	}

	public void SelectNextQuestion()
	{
		tX_QuestionResult.text = "";
		currentQuestionIndex = questionList[Random.Range(0, questionList.Count)];
		questionList.Remove(currentQuestionIndex);
		DisplayQuestion(currentQuestionIndex);
		button_NextQuestion.SetActive(false);
	}

	void DisplayQuestion(int i)
	{
		ReshuffleArray(answerOrders);
		//Display the question
		tX_QuestionContent.text = "Q." + jsonLoader.currentJSON[i]["question"];
		tX_Answers[0].text = jsonLoader.currentJSON[i][answerOrders[0]];
		tX_Answers[1].text = jsonLoader.currentJSON[i][answerOrders[1]];
		tX_Answers[2].text = jsonLoader.currentJSON[i][answerOrders[2]];
		tX_Answers[3].text = jsonLoader.currentJSON[i][answerOrders[3]];

		foreach (GameObject g in button_Answers)
		{
			g.SetActive(true);
		}
	}

	string[] ReshuffleArray(string[] stringArray)
	{
		for (int i = 0; i < stringArray.Length; i++)
		{
			string tmp = stringArray[i];
			int newID = Random.Range(0, stringArray.Length);
			stringArray[i] = stringArray[newID];
			stringArray[newID] = tmp;
		}
		return stringArray;
	}

	public void ButtonPressed_Answer(int answerID)
	{
		//If the answer is correct?
		string answer = answerOrders[answerID];
		if (jsonLoader.currentJSON[currentQuestionIndex]["answer"] == answer)
		{
			//Right answer

			score++;
			tX_QuestionResult.text = "Correct!";
		}
		else
		{
			//Wrong answer
			//reward_BExaminationRoom = 0;
			tX_QuestionResult.text = "Wrong!";
		}
		//Hide choice buttons
		foreach (GameObject g in button_Answers)
		{
			g.SetActive(false);
		}
		button_NextQuestion.SetActive(true);
		tX_Score.text = "Score: " + score;
	}
}
