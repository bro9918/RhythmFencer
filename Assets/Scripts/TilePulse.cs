using UnityEngine;
using System.Collections;

public class TilePulse : MonoBehaviour {

	public bool isOn;
	public bool isBig;
	private Vector3 myScale;
	private Vector3 bigScale;

	// Use this for initialization
	void Start () {
	
		myScale = new Vector3(0.8f,0.2f,1);
		bigScale = new Vector3(1,0.22f,1);

		if(isOn)
		{
			transform.localScale = bigScale;
			isBig = true;
		}
		if(!isOn)
		{
			transform.localScale = myScale;
			isBig = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(!isBig)
		{
			Invoke("TurnBig",0.5f);
		}
		if(isBig)
		{
			Invoke("TurnSmall",0.5f);
		}
	
	}

	public void TurnBig()
	{
		transform.localScale = bigScale;
		isBig = true;
	}
	public void TurnSmall()
	{
		transform.localScale = myScale;
		isBig = false;
	}
}
