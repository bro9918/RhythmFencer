using UnityEngine;
using System.Collections;

public class BeatVisual : MonoBehaviour {

	public LayerMask beatMoverLayer = 0;
	public bool isCenter = false;

	void OnTriggerEnter(Collider other)
	{
		int layer = (int)Mathf.Pow(2, other.gameObject.layer);
		if ((beatMoverLayer.value & layer) == layer)
		{
			transform.position += new Vector3(0, BeatManager.Instance.beatVisualMove, 0);
		}
	}

	void OnTriggerExit(Collider other)
	{
		int layer = (int)Mathf.Pow(2, other.gameObject.layer);
		if ((beatMoverLayer.value & layer) == layer)
		{
			if (!isCenter)
			{
				transform.position -= new Vector3(0, BeatManager.Instance.beatVisualMove, 0);
			}
		}
	}
}
