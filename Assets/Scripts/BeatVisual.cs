using UnityEngine;
using System.Collections;

public class BeatVisual : MonoBehaviour {

	public LayerMask beatMoverLayer = 0;

	void OnTriggerEnter(Collider other)
	{
		if (Mathf.Pow(2, other.gameObject.layer) == beatMoverLayer.value)
		{
			transform.position += new Vector3(0, BeatManager.Instance.beatVisualMove, 0);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (Mathf.Pow(2, other.gameObject.layer) == beatMoverLayer.value)
		{
			transform.position -= new Vector3(0, BeatManager.Instance.beatVisualMove, 0);
		}
	}
}
