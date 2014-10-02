using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatManager : MonoBehaviour {

	private static BeatManager instance;
	public static BeatManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<BeatManager>();
			}
			return instance;
		}
	}
	public int bpm = 120;
	public AudioSource beat;
	private float elapsedSinceBeat;
	public List<GameObject> beatMovers;

	void FixedUpdate()
	{
		float beatPeriod = 60.0f / bpm;


		if (rhythmFader != null)
		{
			Color fadeColor = rhythmFader.renderer.material.color;
			fadeColor.a = elapsedSinceBeat / beatPeriod;
			Debug.Log(fadeColor);
			rhythmFader.renderer.material.color = fadeColor;
		}
		

		if (elapsedSinceBeat >= beatPeriod)
		{
			elapsedSinceBeat = 0;
			if (beat != null)
			{
				beat.Play();
			}
		}
		else
		{
			elapsedSinceBeat += Time.fixedDeltaTime;
		}
	}
}
