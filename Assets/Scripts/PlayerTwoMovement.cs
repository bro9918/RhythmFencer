using UnityEngine;
using System.Collections;

public class PlayerTwoMovement : MonoBehaviour {

	public float xPosition;
	private PlayerOneMovement pOneMove;
	private GameObject pOne;
	public bool moveLeft;
	public bool moveRight;
	public bool attackHigh;
	public bool attackLow;
	public bool actedEarly = false;
	[HideInInspector]
	public float minX;

	// Use this for initialization
	void Start () {
		xPosition = transform.position.x;
		pOne = GameObject.FindGameObjectWithTag("Player1");
		pOneMove = pOne.GetComponent<PlayerOneMovement>();
		minX = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (!moveLeft && !moveRight && !attackHigh && !attackLow && !actedEarly)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (BeatManager.Instance.IsBeatReady())
				{
					moveLeft = true;
				}
				else
				{
					actedEarly = true;
				}
			}

			if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (BeatManager.Instance.IsBeatReady())
				{
					moveRight = true;
				}
				else
				{
					actedEarly = true;
				}
			}
		}
	}
	
	public bool OnBeat(bool ableToUpdate)
	{
		bool acted = false;
		if (ableToUpdate)
		{
			if (moveLeft)
			{
				if (xPosition > minX && (xPosition - pOneMove.xPosition) >= 1.5f)
				{
					transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
					xPosition -= 1.0f;
				}
				
				acted = true;
			}
			else if (moveRight)
			{
				if (xPosition < 4.3f)
				{
					transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
					xPosition += 1.0f;
				}
				acted = true;
			}
		}

		moveLeft = moveRight = attackHigh = attackLow = false;
		actedEarly = false;
		return acted;
	}
}
