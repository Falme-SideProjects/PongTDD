using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	MainMenu,
	Gameplay,
	EndGame
}


public class GameManager : MonoBehaviour, IGameManager
{
	GameManagerController gameManagerController;

	public GameState gameState { get; set; }

	public void FinishedGame()
	{
		gameManagerController.FinishedGame();
	}

	public GameState GetGameState()
	{
		return gameManagerController.GetGameState();
	}

	public void GoToMenu()
	{
		gameManagerController.GoToMenu();

	}

	public void StartGame()
	{
		gameManagerController.StartGame();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			StartGame();
		}
	}
}

public class GameManagerController
{
	//-------INTERFACE----------

	public IGameManager gameManager;

	public void SetGameManager(IGameManager gameManager)
	{
		this.gameManager = gameManager;
	}

	//------END INTERFACE-------



	public void FinishedGame()
	{
		if (gameManager.gameState.Equals(GameState.Gameplay))
		{
			gameManager.gameState = GameState.EndGame;
		}
	}

	public void GoToMenu()
	{
		gameManager.gameState = GameState.MainMenu;
	}

	public void StartGame()
	{
		gameManager.gameState = GameState.Gameplay;
	}

	public GameState GetGameState()
	{
		return gameManager.gameState;
	}
}

public interface IGameManager
{
	GameState gameState { get; set; }

	void StartGame();
	void GoToMenu();
	void FinishedGame();
	GameState GetGameState();
}