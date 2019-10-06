using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IBall
{
	//Public Unity

	[SerializeField]
	private float _ballVelocity;

	[SerializeField]
	private Score[] _Scores;


	//Local Public
	BallController ballController;

	public bool isMoving { get; set; }
	public Vector3 localPosition { get; set; }
	public Vector2 ballDirection { get; set; }
	public float localDeltaTime { get; set; }

	public float ballVelocity { get { return _ballVelocity; } set { _ballVelocity = value; } }

	public Score[] Scores { get { return _Scores; } set { _Scores = value; } }

	public bool GetIsMoving()
	{
		return this.ballController.GetIsMoving();
	}

	public Vector3 GetPosition()
	{
		return ballController.GetPosition();
	}

	public void ResetPosition()
	{
		ballController.ResetPosition();
	}
	
	public void StartMoving()
	{
		this.ballController.StartMoving();
	}

	public void StopMoving()
	{
		this.ballController.StopMoving();
	}

	public void Init()
	{
		this.ballController.Init();
	}

	public void SetPosition(Vector3 position)
	{
		this.ballController.SetPosition(position);
	}

	public void SetDirection(Vector2 direction)
	{
		this.ballController.SetDirection(direction);
	}

	public void RandomizeDirection()
	{
		this.ballController.RandomizeDirection();
	}

	public Vector2 GetDirection()
	{
		return this.ballController.GetDirection();
	}

	public void MoveBall()
	{
		this.ballController.MoveBall();
	}

	public void StartMatch()
	{
		this.ballController.StartMatch();
	}

	public void ReflectHorizontally()
	{
		this.ballController.ReflectHorizontally();
	}

	public void ReflectVertically()
	{
		this.ballController.ReflectVertically();
	}

	public void GoalCollided()
	{
		this.ballController.GoalCollided();
	}

	//--------------------

	private void Awake()
	{
		this.ballController = new BallController();
		this.ballController.SetiBall(this);

		this.ballController.Init();
	}

	private void Update()
	{
		this.ballController.SetLocalDeltaTime(Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.Space)) this.ballController.StartMatch();

		this.ballController.Update(Time.deltaTime);
		if (localPosition != transform.position) transform.position = localPosition;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Collided");
		if(collision.CompareTag("Wall"))
		{
			this.ReflectHorizontally();
		} else if(collision.CompareTag("Racket"))
		{
			this.ReflectVertically();
		} else if (collision.CompareTag("Goal"))
		{
			GoalCollided();
		}
	}

}

public class BallController
{
	private bool runningTimer;
	private float timer;

	IBall iBall;

	public void SetiBall(IBall iBall)
	{
		this.iBall = iBall;
	}

	public void Init()
	{
		iBall.localPosition = Vector3.zero;
		RandomizeDirection();
		if (iBall.Scores.Length ==0) iBall.Scores = new Score[2];
	}

	public void Update(float deltaTime)
	{
		if(runningTimer)
		{
			timer += deltaTime;
			if(timer>= 1f)
			{
				runningTimer = false;
				timer = 0;
				StartMoving();
			}
		}
		
		MoveBall();
		
	}

	public void SetPosition(Vector3 position)
	{
		iBall.localPosition = position;
	}

	public Vector3 GetPosition()
	{
		return iBall.localPosition;
	}

	public void ResetPosition()
	{
		iBall.localPosition = new Vector3(0, 0, 0);
	}

	public bool GetIsMoving()
	{
		return iBall.isMoving;
	}

	public void StartMoving()
	{
		iBall.isMoving = true;
	}

	public void StopMoving()
	{
		iBall.isMoving = false;
	}

	public void MoveBall()
	{
		if( GetIsMoving() )
		iBall.localPosition += ((Vector3)GetDirection() * iBall.ballVelocity * iBall.localDeltaTime);
	}

	public void SetBallVelocity(float velocity)
	{
		iBall.ballVelocity = velocity;
	}

	public float GetBallVelocity()
	{
		return iBall.ballVelocity;
	}

	public void SetLocalDeltaTime(float deltaTime)
	{
		iBall.localDeltaTime = deltaTime;
	}


	public void SetDirection(Vector2 direction)
	{
		iBall.ballDirection = direction;
	}
	

	public void RandomizeDirection()
	{
		int _rnd = Random.Range(0, 2);

		float _x;

		if(GetDirection().x == 0)
		{
			_x = (_rnd == 0 ? 1 : -1);
		} else
		{
			_x = GetDirection().x - 1;
		}

		_rnd = Random.Range(0, 2);

		float _y = (_rnd == 0 ? 1 : -1);

		Vector2 _direction = new Vector2(_x, _y);

		iBall.ballDirection = _direction;
	}

	public Vector2 GetDirection()
	{
		return iBall.ballDirection;
	}

	public void ReflectHorizontally()
	{
		this.SetDirection(new Vector2(this.GetDirection().x, this.GetDirection().y * -1));
	}

	public void ReflectVertically()
	{
		this.SetDirection(new Vector2(this.GetDirection().x*-1, this.GetDirection().y));
		this.IncreaseBallVertically();
	}

	public void IncreaseBallVertically()
	{
		this.SetBallVelocity(GetBallVelocity() * 1.01f);
	}


	public void StartMatch()
	{
		this.runningTimer = true;
	}

	// GOAL


	public void GoalCollided()
	{
		if (GetPosition().x > 0) GoalRight();
		else GoalLeft();

		AfterGoal();
	}

	public void GoalLeft()
	{
		iBall.Scores[1].AddPoint();
	}

	public void GoalRight()
	{
		iBall.Scores[0].AddPoint();
	}

	private void AfterGoal()
	{
		iBall.isMoving = false;
		ResetPosition();
	}

	//Score

	public void SetScore(int index, Score score) { iBall.Scores[index] = score; }
	public Score GetScore(int index) { return iBall.Scores[index]; }

}

public interface IBall
{
	Vector2 ballDirection { get; set; }
	Vector3 localPosition { get; set; }
	bool isMoving { get; set; }
	float ballVelocity { get; set; }
	float localDeltaTime { get; set; }

	Score[] Scores { get; set; }


	bool GetIsMoving();
	
	Vector3 GetPosition();

	void Init();
	
	void StartMoving();
	void StopMoving();

	void SetPosition(Vector3 position);
	void ResetPosition();

	void MoveBall();

	void StartMatch();

	// Ball Direction
	Vector2 GetDirection();
	void SetDirection(Vector2 direction);
	void RandomizeDirection();
	void ReflectHorizontally();
	void ReflectVertically();

	//Goal
	void GoalCollided();


}