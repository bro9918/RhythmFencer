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
	[HideInInspector]
	public float maxX;

	public AudioClip slide;

	// Use this for initialization
	void Start () {
		xPosition = transform.position.x;
		pTwo = GameObject.FindGameObjectWithTag("Player2");
		pTwoMove = pTwo.GetComponent<PlayerTwoMovement>();
		maxX = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (!moveLeft &&! moveRight && !attackHigh && !attackLow && !actedEarly)
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

	public bool OnBeat(bool ableToUpdate)
	{
		bool acted = false;
		if (ableToUpdate)
		{
			
			if (moveLeft)
			{
				if (xPosition > -4.3f)
				{
					transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
					xPosition -= 1.0f;
					audio.clip = slide;
					audio.Play();
				}
				acted = true;
			}
			else if (moveRight)
			{
				if (xPosition < maxX && (pTwoMove.xPosition - xPosition) >= 1.5f)
				{
					transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
					xPosition += 1.0f;
					audio.clip = slide;
					audio.Play();
				}
				acted = true;
			}
		}

		moveLeft = moveRight = attackHigh = attackLow = false;
		actedEarly = false;
		return acted;
	}
}
