using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

	private GameObject pOne;
	private GameObject pTwo;
	private PlayerOneAttack pOneAttack;
	private PlayerTwoAttack pTwoAttack;
	public int pOneScore = 0;
	public int pTwoScore = 0;
	public GUIText pOneScoreText;
	public GUIText pTwoScoreText;
	private PlayerOneMovement pOneMove;
	private PlayerTwoMovement pTwoMove;
	private Vector3 pOneStartSpace;
	private Vector3 pTwoStartSpace;


	// Use this for initialization
	void Start () {
		pOne = GameObject.FindGameObjectWithTag("Player1");
		pOneAttack = pOne.GetComponent<PlayerOneAttack>();
		pOneMove = pOne.GetComponent<PlayerOneMovement>();
		pTwo = GameObject.FindGameObjectWithTag("Player2");
		pTwoAttack = pTwo.GetComponent<PlayerTwoAttack>();
		pTwoMove = pTwo.GetComponent<PlayerTwoMovement>();

		pOneScoreText.text = "" + pOneScore;
		pTwoScoreText.text = "" + pTwoScore;

		pOneStartSpace = new Vector3(-3.3f, -0.33f, 0);
		pTwoStartSpace = new Vector3(3.3f, -0.33f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResolveAttack(bool pOneAct, bool pTwoAct){
		//Debug.Log(pTwoAttack.attackCommitted);

		// Player One.
		if (pOneAttack.attackCommitted && pOneAttack.attackState == 3)
		{
			pOneAttack.spriteRend.sprite = pOneAttack.highAttack;
		}
		else if (pOneAttack.attackCommitted && pOneAttack.attackState == 1)
		{
			pOneAttack.spriteRend.sprite = pOneAttack.lowAttack;
		}
		else if(pOneAct && !pOneAttack.attackCommitted)
		{
			pOneAttack.attackState = 2;
			pOneAttack.spriteRend.sprite = pOneAttack.midAttack;
		}
		else if(!pOneAct && !pOneAttack.attackCommitted)
		{
			pOneAttack.attackState = 0;
			pOneAttack.spriteRend.sprite = pOneAttack.nullAttack;
		}

		// Player Two.
		if (pTwoAttack.attackCommitted && pTwoAttack.attackState == 3)
		{
			pTwoAttack.spriteRend.sprite = pTwoAttack.highAttack;
		}
		else if (pTwoAttack.attackCommitted && pTwoAttack.attackState == 1)
		{
			pTwoAttack.spriteRend.sprite = pTwoAttack.lowAttack;
		}
		else if (pTwoAct && !pTwoAttack.attackCommitted)
		{
			pTwoAttack.attackState = 2;
			pTwoAttack.spriteRend.sprite = pTwoAttack.midAttack;
		}
		else if(!pTwoAct && !pTwoAttack.attackCommitted)
		{
			pTwoAttack.attackState = 0;
			pTwoAttack.spriteRend.sprite = pTwoAttack.nullAttack;
		}
		if(Mathf.Abs(pOne.transform.position.x - pTwo.transform.position.x) < .7f)
		{
			if(pOneAttack.attackState > pTwoAttack.attackState && (pOneAttack.attackState != 3 || pTwoAttack.attackState != 1))
			{
				pOneScore ++;
				pOne.transform.position = pOneStartSpace;
				pTwo.transform.position = pTwoStartSpace;
				pOneMove.xPosition = pOne.transform.position.x;
				pTwoMove.xPosition = pTwo.transform.position.x;
			}
			if(pOneAttack.attackState == 3 && pTwoAttack.attackState == 1)
			{
				pTwoScore ++;
				pOne.transform.position = pOneStartSpace;
				pTwo.transform.position = pTwoStartSpace;
				pOneMove.xPosition = pOne.transform.position.x;
				pTwoMove.xPosition = pTwo.transform.position.x;
			}
			if(pTwoAttack.attackState > pOneAttack.attackState && (pTwoAttack.attackState != 3 || pOneAttack.attackState != 1))
			{
				pTwoScore ++;
				pOne.transform.position = pOneStartSpace;
				pTwo.transform.position = pTwoStartSpace;
				pOneMove.xPosition = pOne.transform.position.x;
				pTwoMove.xPosition = pTwo.transform.position.x;
			}
			if(pTwoAttack.attackState == 3 && pOneAttack.attackState == 1)
			{
				pOneScore ++;
				pOne.transform.position = pOneStartSpace;
				pTwo.transform.position = pTwoStartSpace;
				pOneMove.xPosition = pOne.transform.position.x;
				pTwoMove.xPosition = pTwo.transform.position.x;
			}
		}
		pOneScoreText.text = "" + pOneScore;
		pTwoScoreText.text = "" + pTwoScore;

		pOneAttack.attackCommitted = false;
		pTwoAttack.attackCommitted = false;
	}
}
