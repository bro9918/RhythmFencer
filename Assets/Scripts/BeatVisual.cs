using UnityEngine;
using System.Collections;

public class BeatVisual : MonoBehaviour {

	public LayerMask beatMoverLayer = 0;

	void OnTriggerEnter(Collider other)
	{
		if (Mathf.Pow(2, other.gameObject.layer) == beatMoverLayer.value)
		{
			renderer.enabled = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (Mathf.Pow(2, other.gameObject.layer) == beatMoverLayer.value)
		{
			renderer.enabled = false;
		}
	}
}
