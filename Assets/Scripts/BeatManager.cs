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
	public GameObject beatMoverPrefab;
	public List<GameObject> leftBeatMovers;
	public List<GameObject> rightBeatMovers;
	public Vector3 beatCenter;
	public float beatMoverOffset;
	public float maxBeatMovers = 1;

	void Awake()
	{
		//CreateBeatMovers();
		for (int i = 0; i < maxBeatMovers; i++)
		{
			CreateBeatMovers();
		}
	}

	void FixedUpdate()
	{
		float beatPeriod = 60.0f / bpm;


		/*if (rhythmFader != null)
		{
			Color fadeColor = rhythmFader.renderer.material.color;
			fadeColor.a = elapsedSinceBeat / beatPeriod;
			Debug.Log(fadeColor);
			rhythmFader.renderer.material.color = fadeColor;
		}*/
		

		if (elapsedSinceBeat >= beatPeriod)
		{
			elapsedSinceBeat = 0;
			if (beat != null)
			{
				beat.Play();
				if (leftBeatMovers.Count > 0)
				{
					GameObject leftBeatMover = leftBeatMovers[0];
					leftBeatMovers.RemoveAt(0);
					Destroy(leftBeatMover);
				}
				if (rightBeatMovers.Count > 0)
				{
					GameObject rightBeatMover = rightBeatMovers[0];
					rightBeatMovers.RemoveAt(0);
					Destroy(rightBeatMover);
				}
				CreateBeatMovers();
			}
		}
		else
		{
			elapsedSinceBeat += Time.fixedDeltaTime;
			//Debug.Log((beatMoverOffset / maxBeatMovers) * (maxBeatMovers - 1));
			if (leftBeatMovers.Count < 1)// || (leftBeatMovers[leftBeatMovers.Count - 1].transform.position - beatCenter).magnitude <= (beatMoverOffset / maxBeatMovers) * (maxBeatMovers - 1))
			{
				//if (leftBeatMovers.Count > 0)
				//	Debug.Log(leftBeatMovers.Count + " " + (leftBeatMovers[leftBeatMovers.Count - 1].transform.position - beatCenter).magnitude);
				CreateBeatMovers();
			}

			for (int i = 0; i < leftBeatMovers.Count; i++)
			{
				leftBeatMovers[i].transform.position += new Vector3((beatMoverOffset / beatPeriod) * Time.fixedDeltaTime, 0, 0);
				rightBeatMovers[i].transform.position -= new Vector3((beatMoverOffset / beatPeriod) * Time.fixedDeltaTime, 0, 0);
			}
		}
	}

	private void CreateBeatMovers()
	{
		GameObject leftMover = (GameObject)Instantiate(beatMoverPrefab, beatCenter + new Vector3(-beatMoverOffset * (leftBeatMovers.Count + 1), 0, 0), Quaternion.identity);
		leftMover.renderer.material.color = Color.red;
		leftBeatMovers.Add(leftMover);
		GameObject rightMover = (GameObject)Instantiate(beatMoverPrefab, beatCenter + new Vector3(beatMoverOffset * (rightBeatMovers.Count + 1), 0, 0), Quaternion.identity);
		rightMover.renderer.material.color = Color.blue;
		rightBeatMovers.Add(rightMover);
	}
}
