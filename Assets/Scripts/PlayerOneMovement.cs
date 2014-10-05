using UnityEngine;
using System.Collections;

public class PlayerOneMovement : MonoBehaviour {

	public float xPosition;
	private PlayerTwoMovement pTwoMove;
	private GameObject pTwo;
	private bool moveLeft;
	public bool moveRight;
	public bool attackHigh;
	public bool attackLow;
	public bool actedEarly = false;

	// Use this for initialization
	void Start () {
		xPosition = transform.position.x;
		pTwo = GameObject.FindGameObjectWithTag("Player2");
		pTwoMove = pTwo.GetComponent<PlayerTwoMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!moveLeft &&! moveRight && !attackHigh && !attackLow && !actedEarly)
		{
			if (xPosition > -4.3f)
			{
				if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
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

			if (xPosition < 1.7f && (pTwoMove.xPosition - xPosition) >= 1.5f)
			{
				if (Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
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
