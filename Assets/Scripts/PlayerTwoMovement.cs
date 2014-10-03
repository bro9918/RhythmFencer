using UnityEngine;
using System.Collections;

public class PlayerTwoMovement : MonoBehaviour {

	public float xPosition = 2.0f;
	private PlayerOneMovement pOneMove;
	private GameObject pOne;

	// Use this for initialization
	void Start () {
		pOne = GameObject.FindGameObjectWithTag("Player1");
		pOneMove = pOne.GetComponent<PlayerOneMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(xPosition > -2.0f && (xPosition - pOneMove.xPosition) >= 2.0f)
		{
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
				xPosition -= 1.0f;
			}
		}
		
		if(xPosition < 4.0f)
		{
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
				xPosition += 1.0f;
			}
		}
	}
}
