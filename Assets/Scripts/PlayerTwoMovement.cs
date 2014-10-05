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

	// Use this for initialization
	void Start () {
		xPosition = transform.position.x;
		pOne = GameObject.FindGameObjectWithTag("Player1");
		pOneMove = pOne.GetComponent<PlayerOneMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!moveLeft && !moveRight && !attackHigh && !attackLow && !actedEarly)
		{
			if (xPosition > -1.7f && (xPosition - pOneMove.xPosition) >= 1.5f)
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
			}

			if (xPosition < 4.3f)
			{
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
	}
	
	public bool OnBeat(bool ableToUpdate)
	{
		bool acted = false;
		if (ableToUpdate)
		{
			if (moveLeft)
			{
				transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
				xPosition -= 1.0f;
				acted = true;
			}
			else if (moveRight)
			{
				transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
				xPosition += 1.0f;
				acted = true;
			}
		}

		moveLeft = moveRight = attackHigh = attackLow = false;
		actedEarly = false;
		return acted;
	}
}
