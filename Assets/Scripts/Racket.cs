using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour, IRacket
{
	//Unity Public
	public float _racketVelocity;
	public float _racketLimit;



	//Local Public
	RacketController racketController;

	public Vector2 localPosition { get; set; }


	public float racketVelocity
	{
		get { return this._racketVelocity; }
		set { this._racketVelocity = value; }
	}

	public float localDeltaTime { get; set; }

	public float racketLimit
	{
		get { return this._racketLimit; }
		set { this._racketLimit = value; }
	}

	public Vector2 GetPosition()
	{
		return racketController.GetPosition();
	}
	
	public void MoveDown()
	{
		racketController.MoveDown();
	}

	public void MoveUp()
	{
		racketController.MoveUp();
	}

	public void SetPosition(Vector2 position)
	{
		racketController.SetPosition(position);
	}


	public void SetRacketVelocity(float velocity)
	{
		racketController.SetRacketVelocity(velocity);
	}

	public void SetLocalDeltaTime(float deltaTime)
	{
		racketController.SetLocalDeltaTime(deltaTime);
	}


	//--------------------------------

	void Start ()
	{
		racketController = new RacketController();
		racketController.SetIRacket(this);
		SetPosition(transform.position);
	}
	
	void Update ()
	{
		SetLocalDeltaTime(Time.deltaTime);
		if (Input.GetKey(KeyCode.UpArrow)) MoveUp();
		if (Input.GetKey(KeyCode.DownArrow)) MoveDown();
		transform.position = GetPosition();
	}
}

public class RacketController
{
	IRacket iRacket;

	public void SetIRacket(IRacket iRacket)
	{
		this.iRacket = iRacket;
	}

	public Vector2 GetPosition()
	{
		return iRacket.localPosition;
	}

	public void SetPosition(Vector2 position)
	{
		iRacket.localPosition = position;
	}

	public void SetRacketLimit(float limit)
	{
		iRacket.racketLimit = limit;
	}
	

	public void MoveDown()
	{
		VerifyLimit();
		iRacket.localPosition -= Vector2.up * iRacket.racketVelocity * iRacket.localDeltaTime;
		VerifyLimit();
	}

	public void MoveUp()
	{
		VerifyLimit();
		iRacket.localPosition += Vector2.up * iRacket.racketVelocity * iRacket.localDeltaTime;
		VerifyLimit();
	}

	private void VerifyLimit()
	{
		if (iRacket.localPosition.y > iRacket.racketLimit)
		{
			iRacket.localPosition = new Vector2(iRacket.localPosition.x, iRacket.racketLimit);
		} else if(iRacket.localPosition.y < -iRacket.racketLimit)
		{
			iRacket.localPosition = new Vector2(iRacket.localPosition.x, -iRacket.racketLimit);
		}
	}


	public void SetRacketVelocity(float velocity)
	{
		iRacket.racketVelocity = velocity;
	}

	public void SetLocalDeltaTime(float deltaTime)
	{
		iRacket.localDeltaTime = deltaTime;
	}


}

public interface IRacket
{
	float racketVelocity { get; set; }
	float racketLimit { get; set; }
	void SetRacketVelocity(float velocity);

	float localDeltaTime { get; set; }
	void SetLocalDeltaTime(float deltaTime);


	Vector2 localPosition { get; set; }
	Vector2 GetPosition();
	void SetPosition(Vector2 position);

	void MoveUp();
	void MoveDown();
}

