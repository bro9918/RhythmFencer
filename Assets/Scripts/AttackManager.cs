using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

	private static AttackManager instance;
	public static AttackManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<AttackManager>();
			}
			return instance;
		}
	}
	private GameObject pOne;
	private GameObject pTwo;
	private PlayerOneAttack pOneAttack;
	private PlayerTwoAttack pTwoAttack;
	public int pOneScore = 0;
	public int pOneAggressiveBeats = 0;
	public int pTwoScore = 0;
	public int pTwoAggressiveBeats = 0;
	public GUIText pOneScoreText;
	public GUIText pTwoScoreText;
	private PlayerOneMovement pOneMove;
	private PlayerTwoMovement pTwoMove;
	public Vector3 pOneStartSpace;
	public Vector3 pTwoStartSpace;
	public bool resetingPositions = false;


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

		pOneStartSpace = new Vector3(-3f, -0.33f, 0);
		pTwoStartSpace = new Vector3(3f, -0.33f, 0);
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
		else if (pOneAct && !pOneAttack.attackCommitted && !pOneAttack.parried)
		{
			pOneAttack.attackState = 2;
			pOneAttack.spriteRend.sprite = pOneAttack.midAttack;
		}
		else
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
		else if (pTwoAct && !pTwoAttack.attackCommitted && !pTwoAttack.parried)
		{
			pTwoAttack.attackState = 2;
			pTwoAttack.spriteRend.sprite = pTwoAttack.midAttack;
		}
		else
		{
			pTwoAttack.attackState = 0;
			pTwoAttack.spriteRend.sprite = pTwoAttack.nullAttack;
		}

		pOneAttack.parried = false;
		pTwoAttack.parried = false;

		if(Mathf.Abs(pOne.transform.position.x - pTwo.transform.position.x) < .7f)
		{
			if(pOneAttack.attackState > pTwoAttack.attackState && (pOneAttack.attackState != 3 || pTwoAttack.attackState != 1))
			{
				
				if (pOneAttack.attackState == 3)
				{
					pTwoAttack.parried = true;
					pTwoAttack.spriteRend.sprite = pTwoAttack.parriedAttack;
				}
				else
				{
					pOneScore++;
					resetingPositions = true;
				}
				
			}
			if(pOneAttack.attackState == 3 && pTwoAttack.attackState == 1)
			{
				pTwoScore ++;
				resetingPositions = true;
			}
			if(pTwoAttack.attackState > pOneAttack.attackState && (pTwoAttack.attackState != 3 || pOneAttack.attackState != 1))
			{
				if (pTwoAttack.attackState == 3)
				{
					pOneAttack.parried = true;
					pOneAttack.spriteRend.sprite = pOneAttack.parriedAttack;
				}
				else
				{
					pTwoScore++;
					resetingPositions = true;
				}
			}
			if(pTwoAttack.attackState == 3 && pOneAttack.attackState == 1)
			{
				pOneScore ++;
				resetingPositions = true;
			}

			if (resetingPositions)
			{
				pOneMove.maxX = -3;
				pTwoMove.minX = 3;
				BeatManager.Instance.pOneBackedUp = BeatManager.Instance.pTwoBackedUp = false;
			}
		}
		pOneScoreText.text = "" + pOneScore;
		pTwoScoreText.text = "" + pTwoScore;

		pOneAttack.attackCommitted = false;
		pTwoAttack.attackCommitted = false;
	}

	public void ApplyAggressiveBeats()
	{
		if (pOneAggressiveBeats >= BeatManager.Instance.aggressiveBeatsToPoint && BeatManager.Instance.aggressiveBeatsToPoint > 0)
		{
			pOneAggressiveBeats = 0;
			pOneScore++;
		}

		if (pTwoAggressiveBeats >= BeatManager.Instance.aggressiveBeatsToPoint && BeatManager.Instance.aggressiveBeatsToPoint > 0)
		{
			pTwoAggressiveBeats = 0;
			pTwoScore++;
		}
	}
}
