using UnityEngine;
using System.Collections;

public class PlayerOneMovement : MonoBehaviour {

	public float xPosition = -3.0f;
	private PlayerTwoMovement pTwoMove;
	private GameObject pTwo;

	// Use this for initialization
	void Start () {
		pTwo = GameObject.FindGameObjectWithTag("Player2");
		pTwoMove = pTwo.GetComponent<PlayerTwoMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(xPosition >-5.0f)
		{
			if(Input.GetKeyDown(KeyCode.A))
			{
				transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
				xPosition -= 1.0f;
			}
		}

		if(xPosition < 1.0f && (pTwoMove.xPosition - xPosition) >= 2.0f)
		{
			if(Input.GetKeyDown(KeyCode.D))
			{
				transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
				xPosition += 1.0f;
			}
		}
	}
}
