using UnityEngine;
using System.Collections;

public class PlayerTwoAttack : MonoBehaviour {
	
	public SpriteRenderer spriteRend;
	public int attackState;
	public bool attackCommitted;
	public Sprite lowAttack;
	public Sprite midAttack;
	public Sprite highAttack;
	public Sprite nullAttack;
	
	// Use this for initialization
	void Start () {
		spriteRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!attackCommitted && Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(BeatManager.Instance.IsBeatReady())
			{
				attackState = 3;
				attackCommitted = true;
			}
		}
		if(!attackCommitted && Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(BeatManager.Instance.IsBeatReady())
			{
				attackState = 1;
				attackCommitted = true;
			}
		}

	}
}
