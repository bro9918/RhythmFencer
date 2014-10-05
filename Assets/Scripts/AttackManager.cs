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


	// Use this for initialization
	void Start () {
		pOne = GameObject.FindGameObjectWithTag("Player1");
		pOneAttack = pOne.GetComponent<PlayerOneAttack>();
		pTwo = GameObject.FindGameObjectWithTag("Player2");
		pTwoAttack = pTwo.GetComponent<PlayerTwoAttack>();

		pOneScoreText.text = "" + pOneScore;
		pTwoScoreText.text = "" + pTwoScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResolveAttack(bool pOneAct, bool pTwoAct){
		Debug.Log(pTwoAttack.attackCommitted);


		if(pOneAct && !pOneAttack.attackCommitted)
		{
			pOneAttack.attackState = 2;
			pOneAttack.spriteRend.sprite = pOneAttack.midAttack;
		}
		else if(!pOneAct && !pOneAttack.attackCommitted)
		{
			pOneAttack.attackState = 0;
		}
		if(pTwoAct && !pTwoAttack.attackCommitted)
		{
			pTwoAttack.attackState = 2;
			pTwoAttack.spriteRend.sprite = pTwoAttack.midAttack;
		}
		else if(!pTwoAct && !pTwoAttack.attackCommitted)
		{
			pTwoAttack.attackState = 0;
		}
		if(Mathf.Abs(pOne.transform.position.x - pTwo.transform.position.x) < .7f)
		{
			if(pOneAttack.attackState > pTwoAttack.attackState && pTwoAttack.attackState != 1)
				pOneScore ++;
			if(pOneAttack.attackState == 3 && pTwoAttack.attackState == 1)
				pTwoScore ++;
			if(pTwoAttack.attackState > pOneAttack.attackState && pOneAttack.attackState != 1)
				pTwoScore ++;
			if(pTwoAttack.attackState == 3 && pOneAttack.attackState == 1)
				pOneScore ++;
		}
		pOneScoreText.text = "" + pOneScore;
		pTwoScoreText.text = "" + pTwoScore;

		pOneAttack.attackCommitted = false;
		pTwoAttack.attackCommitted = false;
	}
}
