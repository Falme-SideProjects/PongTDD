using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class GameManagerTests {

	IGameManager _iGameManager;
	GameManagerController _gameManager;

	[SetUp]
	public void SetupGameManager()
	{
		_iGameManager = Substitute.For<IGameManager>();
		_gameManager = new GameManagerController();
		_gameManager.SetGameManager(_iGameManager);
	}

    [Test]
    public void VerifyIfGameStateInitializeMainMenu()
	{
		Assert.AreEqual(GameState.MainMenu, _gameManager.GetGameState());
    }

	[Test]
	public void StartGame_ChangeGameStateToGameplay()
	{
		_gameManager.StartGame();

		Assert.AreEqual(GameState.Gameplay, _gameManager.GetGameState());
	}

	[Test]
	public void FinishedGame_ChangeGameStateToEndGame()
	{
		_gameManager.StartGame();
		_gameManager.FinishedGame();

		Assert.AreEqual(GameState.EndGame, _gameManager.GetGameState());
	}

	[Test]
	public void FinishedGame_NOTChangeGameStateToEndGameIfOnMenu()
	{
		_gameManager.FinishedGame();

		Assert.AreEqual(GameState.MainMenu, _gameManager.GetGameState());
	}
}
