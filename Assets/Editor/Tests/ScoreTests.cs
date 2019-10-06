using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using TMPro;

public class ScoreTests
{
	IScore iScore;
	ScoreController scoreController;

	[SetUp]
	public void SetUpScore()
	{
		iScore = Substitute.For<IScore>();
		scoreController = new ScoreController();
		scoreController.SetIScore(iScore);
		scoreController.SetTextUI(Substitute.For<TextMeshProUGUI>());
		scoreController.Init();
	}


    [Test]
    public void Init_StartPointsWithZero()
	{
		Assert.AreEqual(0, scoreController.GetPoints());
    }

	[Test]
	public void AddPoint_AddOnePointToScore()
	{
		scoreController.AddPoint();

		Assert.AreEqual(1, scoreController.GetPoints());
	}

	[Test]
	public void AddPoint_SetTextUIToScore()
	{
		scoreController.AddPoint();

		Assert.AreEqual("1", scoreController.GetTextUIText());
	}

	[Test]
	public void ResetPoint_SetPointsToZero()
	{
		scoreController.SetPoints(9);

		scoreController.ResetPoint();

		Assert.AreEqual(0, scoreController.GetPoints());
	}

	[Test]
	public void ResetPoint_SetTextUIToZero()
	{
		scoreController.SetPoints(9);

		scoreController.ResetPoint();

		Assert.AreEqual("0", scoreController.GetTextUIText());
	}

}
