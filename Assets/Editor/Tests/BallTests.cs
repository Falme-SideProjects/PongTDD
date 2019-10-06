using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using TMPro;

public class BallTests {

	IBall iBall;
	BallController ballController;

	[SetUp]
	public void SetupBallTests()
	{
		iBall = Substitute.For<IBall>();
		ballController = new BallController();
		ballController.SetiBall(iBall);
		ballController.Init();
		ballController.SetBallVelocity(1f);
		ballController.SetLocalDeltaTime(1f);
	}

    [Test]
    public void Init_BallNotMoving()
	{
		Assert.AreEqual(false, ballController.GetIsMoving());
    }



	[Test]
	public void StartMoving_BallSetToMoving()
	{
		ballController.StartMoving();

		Assert.AreEqual(true, ballController.GetIsMoving());
	}



	[Test]
	public void StopMoving_BallSetToNotMove()
	{
		ballController.StartMoving();
		ballController.StopMoving();

		Assert.AreEqual(false, ballController.GetIsMoving());
	}

	[Test]
	public void Init_StartBallAtCenter()
	{
		Assert.AreEqual(Vector3.zero, ballController.GetPosition());
	}

	[Test]
	public void ResetPosition_ResetBallToCenter()
	{
		ballController.SetPosition(new Vector3(0,1,0));
		ballController.ResetPosition();

		Assert.AreEqual(Vector3.zero, ballController.GetPosition());
	}

	[Test]
	public void Init_StartWithNonZeroDirection()
	{
		Assert.AreNotEqual(Vector2.zero, ballController.GetDirection());
	}

	[Test]
	public void RandomizeDirection_OnRandomizeGetDifferentFromLastValue()
	{
		for(int a=0; a<20; a++)
		{
			Vector2 _lastSaved = ballController.GetDirection();

			ballController.RandomizeDirection();

			Assert.AreNotEqual(_lastSaved, ballController.GetDirection());
		}
	}

	[Test]
	public void MoveBall_ChangePositionWhenMoveBall()
	{
		ballController.StartMoving();
		ballController.MoveBall();

		Assert.AreNotEqual(Vector3.zero, ballController.GetPosition());
	}

	[Test]
	public void MoveBall_ChangePositionIfIsMoving()
	{
		ballController.StartMoving();

		ballController.MoveBall();

		Assert.AreNotEqual(Vector3.zero, ballController.GetPosition());
	}


	[Test]
	public void MoveBall_NOTChangePositionIfNOTIsMoving()
	{
		ballController.StopMoving();

		ballController.MoveBall();

		Assert.AreEqual(Vector3.zero, ballController.GetPosition());
	}

	[Test]
	public void ReflectHorizontally_InvertYDirectionWhenHitWall()
	{
		var _expected = new Vector2(1, -1);

		ballController.SetDirection(new Vector2(1, 1));

		ballController.ReflectHorizontally();

		Assert.AreEqual(_expected, ballController.GetDirection());
	}

	[Test]
	public void ReflectVertically_InvertXDirectionWhenHitRacket()
	{
		var _expected = new Vector2(-1, 1);

		ballController.SetDirection(new Vector2(1, 1));

		ballController.ReflectVertically();

		Assert.AreEqual(_expected, ballController.GetDirection());
	}

	[Test]
	public void IncreaseBallVertically_WhenHitRacketIncreaseBallVelocity()
	{
		var _expected = ballController.GetBallVelocity();

		ballController.ReflectVertically();

		Assert.AreNotEqual(_expected, ballController.GetBallVelocity());
	}

	[Test]
	public void GoalLeft_OnHitAddScoreToRightPlayer()
	{
		SetBallControllerScoreData(1);


		ballController.GoalLeft();

		Assert.AreEqual(1, ballController.GetScore(1).GetPoints());
	}


	[Test]
	public void GoalRight_OnHitAddScoreToLeftPlayer()
	{
		SetBallControllerScoreData(0);

		ballController.GoalRight();

		Assert.AreEqual(1, ballController.GetScore(0).GetPoints());
	}

	/*[Test]
	public void GoalCollided_OnHitIsNotMoving()
	{
		SetBallControllerScoreData(0);

		ballController.StartMoving();

		ballController.MoveBall();

		ballController.GoalCollided();

		Assert.AreEqual(false, ballController.GetIsMoving());
	}*/


	private void SetBallControllerScoreData(int index)
	{
		ballController.SetScore(0, Substitute.For<Score>());
		ballController.SetScore(1, Substitute.For<Score>());

		ballController.GetScore(index).scoreController = new ScoreController();
		ballController.GetScore(index).scoreController.SetIScore(Substitute.For<IScore>());


		ballController.GetScore(index).scoreController.SetTextUI(Substitute.For<TextMeshProUGUI>());
	}


	//---------------------------------------------------------------------


	/*[UnityTest]
    public IEnumerator StartMatch_NOTChangePositionUntilOneSecond()
	{
		ballController.StartMatch();

		yield return new WaitForSeconds(0.99f);

		Assert.AreEqual(Vector3.zero, ballController.GetPosition());
	}


	[UnityTest]
	public IEnumerator StartMatch_ChangePositionAfterOneSecond()
	{
		ballController.StartMatch();

		yield return new WaitForSeconds(1.1f);

		Assert.AreEqual(Vector3.zero, ballController.GetPosition());
	}*/
}
