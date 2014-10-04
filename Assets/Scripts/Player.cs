using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private float xPosition;
	private bool isPlayerOne;
	private Player otherPlayer;
	private float leftBound;
	private float rightBound;

	// Use this for initialization
	void Start()
	{
		isPlayerOne = gameObject.tag == "Player1";
		xPosition = transform.position.x;
		leftBound = xPosition - 2;
		rightBound = xPosition + 2;
	}

	// Update is called once per frame
	void Update()
	{
		if (xPosition > leftBound)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
				xPosition -= 1.0f;
			}
		}

		if (xPosition < 4.0f)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
				xPosition += 1.0f;
			}
		}
	}

	private bool LeftPressed()
	{
		return (isPlayerOne && Input.GetKeyDown(KeyCode.LeftArrow)) || (!isPlayerOne && Input.GetKeyDown(KeyCode.LeftArrow));
	}
}
