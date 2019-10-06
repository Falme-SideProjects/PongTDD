using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour, IScore
{
	public ScoreController scoreController;

	public int Points { get; set; }

	public TextMeshProUGUI TextUI { get; set; }

	public void Init()
	{
		scoreController.Init();
	}

	public void AddPoint()
	{
		scoreController.AddPoint();
	}
	public void ResetPoint()
	{
		scoreController.ResetPoint();
	}

	public int GetPoints()
	{
		return scoreController.GetPoints();
	}

	public void SetPoints(int points)
	{
		scoreController.SetPoints(points);
	}

	//-------------------------------------------
	void Awake ()
	{
		scoreController = new ScoreController();
		scoreController.SetIScore(this);
		scoreController.SetTextUI(GetComponent<TextMeshProUGUI>());
		Init();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) scoreController.AddPoint();
		if (Input.GetKeyDown(KeyCode.Alpha2)) scoreController.ResetPoint();
	}
}

public class ScoreController
{

	IScore score;

	public void SetIScore(IScore score)
	{
		this.score = score;
	}

	public void Init()
	{
		ResetPoint();
	}

	public void SetTextUI(TextMeshProUGUI TextUI)
	{
		score.TextUI = TextUI;
	}

	public string GetTextUIText()
	{
		return score.TextUI.text;
	}

	private void UpdateTextUI()
	{
		score.TextUI.text = score.Points.ToString();
	}


	public void AddPoint()
	{
		score.Points++;
		UpdateTextUI();
	}
	public void ResetPoint()
	{
		score.Points = 0;
		UpdateTextUI();
	}

	public int GetPoints()
	{
		return score.Points;
	}

	public void SetPoints(int points)
	{
		score.Points = points;
		UpdateTextUI();
	}


}

public interface IScore
{
	void Init();

	int Points { get; set; }

	TextMeshProUGUI TextUI { get; set; }

	void SetPoints(int points);
	int GetPoints();

	void AddPoint();
	void ResetPoint();
}