using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	public string sceneToLoad;
	public GameObject pOneReadyImage;
	public GameObject pTwoReadyImage;
	private bool pOneReady = false;
	private bool pTwoReady = false;

	void Awake()
	{
		pOneReadyImage.SetActive(false);
		pTwoReadyImage.SetActive(false);
	}

	void Update () {
		if (pOneReady && pTwoReady)
		{
			Application.LoadLevel(sceneToLoad);
		}

		if (!pOneReady && (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")))
		{
			pOneReady = true;
			pOneReadyImage.SetActive(true);
		}

		if (!pTwoReady && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
		{
			pTwoReady = true;
			pTwoReadyImage.SetActive(true);
		}
	}
}
