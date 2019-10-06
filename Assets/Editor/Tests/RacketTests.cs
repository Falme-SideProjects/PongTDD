using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class RacketTests {

	IRacket iRacket;
	RacketController racketController;

	[SetUp]
	public void SetUpRacket()
	{
		iRacket = Substitute.For<IRacket>();
		racketController = new RacketController();
		racketController.SetIRacket(iRacket);
		racketController.SetLocalDeltaTime(1f);
		racketController.SetRacketVelocity(1f);
		racketController.SetRacketLimit(10);
	}

    [Test]
    public void MoveUp_MoveRacketOneUp()
	{
		Vector2 _expected = new Vector2(0, 1);

		racketController.MoveUp();

		Assert.AreEqual(_expected, racketController.GetPosition());
    }


	[Test]
	public void MoveDown_MoveRacketOneDown()
	{
		Vector2 _expected = new Vector2(0, -1);

		racketController.MoveDown();

		Assert.AreEqual(_expected, racketController.GetPosition());
	}

	[Test]
	[TestCase(50, 50, 50)]
	[TestCase(50, 49, 50)]
	[TestCase(5, 4, 50)]
	[TestCase(5, 49, 5)]
	[TestCase(-49, -60, 50)]
	public void MoveUp_DoNotMoveIfIsLimit(float expectedY, float setY, float limit)
	{

		Vector2 _expected = new Vector2(0, expectedY);
		racketController.SetPosition( new Vector2(0, setY) );
		racketController.SetRacketLimit(limit);
		racketController.MoveUp();

		Assert.AreEqual(_expected, racketController.GetPosition());
	}


	[Test]
	[TestCase(49, 50, 50)]
	[TestCase(-50, -49, 50)]
	[TestCase(-50, -50, 50)]
	[TestCase(-5, -4, 50)]
	[TestCase(-5, -49, -5)]
	[TestCase(49, 60, 50)]
	public void MoveDown_DoNotMoveIfIsLimit(float expectedY, float setY, float limit)
	{

		Vector2 _expected = new Vector2(0, expectedY);
		racketController.SetPosition(new Vector2(0, setY));
		racketController.SetRacketLimit(limit);
		racketController.MoveDown();

		Assert.AreEqual(_expected, racketController.GetPosition());
	}

}
