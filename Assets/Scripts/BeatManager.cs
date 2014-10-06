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
	public bool showBeatMovers = false;
	public GameObject beatMoverPrefab;
	public List<GameObject> leftBeatMovers;
	public List<GameObject> rightBeatMovers;
	public Vector3 beatCenter;
	public float beatMoverOffset;
	public float maxBeatMovers = 1;
	public GameObject centerBeatVisual;
	public float beatVisualMove;
	public PlayerOneMovement playerOne;
	public PlayerTwoMovement playerTwo;
	public float beatTolerance = 0.5f;
	public string leftMoverLayer;
	public string rightMoverLayer;
	private GameObject leftMoverDestroy;
	private GameObject rightMoverDestroy;
	public GameObject invalidSign;
	private AttackManager attackManager;
	//public bool autoBackup = true;
	[HideInInspector]
	public bool pOneBackedUp = false;
	[HideInInspector]
	public bool pTwoBackedUp = false;
	public GameObject beatMoverContatiner;
	public int aggressiveBeatsToPoint;
	public bool resetAggressiveOnMiss;
	public bool resetAggressiveOnBack;

	void Awake()
	{
		if (invalidSign != null)
		{
			invalidSign.SetActive(false);
		}

		for (int i = 0; i < maxBeatMovers - 1; i++)
		{
			CreateBeatMovers();
		}
		attackManager = GetComponent<AttackManager>();
	}

	void FixedUpdate()
	{
		beatTolerance = Mathf.Clamp(beatTolerance, 0, 0.5f);
		float beatPeriod = 60.0f / bpm;

		// Handle beat timing.
		if (elapsedSinceBeat >= beatPeriod)
		{
			elapsedSinceBeat = 0;
			if (beat != null)
			{
				beat.Play();

				if (invalidSign != null)
				{
					invalidSign.SetActive(false);
				}

				// Reset beat tracking geometry.
				if (centerBeatVisual != null)
				{
					centerBeatVisual.transform.position -= new Vector3(0, beatVisualMove * 2, 0);
				}
				if (leftBeatMovers.Count > 0)
				{
					leftMoverDestroy = leftBeatMovers[0];
					rightMoverDestroy = rightBeatMovers[0];
				}
			}
		}
		else
		{
			elapsedSinceBeat += Time.fixedDeltaTime;

			// Spawn new beat movers to keep the gap between at beatMoverOffset.
			if (leftBeatMovers.Count < 1 || (leftBeatMovers[leftBeatMovers.Count - 1].transform.position - beatCenter).magnitude <= beatMoverOffset * (maxBeatMovers - 1))
			{
				CreateBeatMovers();
			}
		}

		// Move all beat movers towards the center.
		for (int i = 0; i < leftBeatMovers.Count; i++)
		{
			leftBeatMovers[i].transform.position += new Vector3((beatMoverOffset / beatPeriod) * Time.fixedDeltaTime, 0, 0);
			rightBeatMovers[i].transform.position -= new Vector3((beatMoverOffset / beatPeriod) * Time.fixedDeltaTime, 0, 0);
		}

		// Track when the oldest beat movers have given enough time after beat.
		if (leftMoverDestroy != null && (leftMoverDestroy.transform.position - beatCenter).magnitude >= beatMoverOffset * beatTolerance)
		{
			// Track if the players have acted on this beat.
			bool pOneAct = false, pTwoAct = false;

			// Destroy the oldest beat movers.
			DestroyBeatMovers();

			// Notify players that beat happened.
			bool playersUpdate= true;
			bool oneSpaceBetween = (playerTwo.transform.position - playerOne.transform.position).magnitude < 2;
			if (oneSpaceBetween && playerOne.moveRight && playerTwo.moveLeft)
			{
				playersUpdate = false;
				// TODO show that players attempted to move into the same space.
				if (invalidSign != null)
				{
					invalidSign.SetActive(true);
					invalidSign.transform.position = (playerOne.transform.position + playerTwo.transform.position) / 2;
				}
			}
			else
			{
				if (invalidSign != null)
				{
					invalidSign.SetActive(false);
				}
			}

			if (!attackManager.resetingPositions)
			{
				pOneAct = playerOne.OnBeat(playersUpdate);
				pTwoAct = playerTwo.OnBeat(playersUpdate);
				attackManager.ResolveAttack(pOneAct, pTwoAct);
			}
			else
			{
				if (true)//autoBackup)
				{
					if (!pOneBackedUp && playerOne.transform.position.x > attackManager.pOneStartSpace.x)
					{
						playerOne.transform.position += new Vector3(-1, 0, 0);
						playerOne.xPosition = playerOne.transform.position.x;
						pOneAct = true;
					}
					else
					{
						pOneBackedUp = true;
						pOneAct = playerOne.OnBeat(true);
						//Debug.Log(pOneAct);
					}
					if (!pTwoBackedUp && playerTwo.transform.position.x < attackManager.pTwoStartSpace.x)
					{
						playerTwo.transform.position += new Vector3(1, 0, 0);
						playerTwo.xPosition = playerTwo.transform.position.x;
						pTwoAct = true;
					}
					else
					{
						pTwoBackedUp = true;
						pTwoAct = playerTwo.OnBeat(true);
						//Debug.Log(pTwoAct);
					}
				}

				if ((pOneBackedUp && pTwoBackedUp) && (pOneAct && pTwoAct))
				{
					attackManager.resetingPositions = false;
					playerOne.maxX = 1;
					playerTwo.minX = -1;
				}
			}

			// Track beats of players standing on opponent's side.
			UpdateAggressiveBeats(pOneAct, pTwoAct);
			attackManager.ApplyAggressiveBeats();
		}
	}

	private void CreateBeatMovers()
	{
		int moverCount = (int)Mathf.Min(leftBeatMovers.Count + 1, maxBeatMovers);

		GameObject leftMover = (GameObject)Instantiate(beatMoverPrefab, beatCenter + new Vector3(-beatMoverOffset * moverCount, 0, 0), Quaternion.identity);
		leftMover.renderer.material.color = Color.red;
		leftMover.renderer.enabled = showBeatMovers;
		leftMover.gameObject.layer = LayerMask.NameToLayer(leftMoverLayer);
		leftBeatMovers.Add(leftMover);
		GameObject rightMover = (GameObject)Instantiate(beatMoverPrefab, beatCenter + new Vector3(beatMoverOffset * moverCount, 0, 0), Quaternion.identity);
		rightMover.renderer.material.color = Color.blue;
		rightMover.renderer.enabled = showBeatMovers;
		rightMover.gameObject.layer = LayerMask.NameToLayer(rightMoverLayer);
		rightBeatMovers.Add(rightMover);

		if (beatMoverContatiner != null)
		{
			leftMover.transform.parent = beatMoverContatiner.transform;
			rightMover.transform.parent = beatMoverContatiner.transform;
		}
	}

	private void DestroyBeatMovers()
	{
		if (leftMoverDestroy != null)
		{
			leftBeatMovers.Remove(leftMoverDestroy);
			Destroy(leftMoverDestroy);
		}
		if (rightMoverDestroy != null)
		{
			rightBeatMovers.Remove(rightMoverDestroy);
			Destroy(rightMoverDestroy);
		}
	}

	public bool IsBeatReady()
	{
		// Return whether or not the oldest beat movers are within range of the beatCenter (beatOffset * beatTolerance).
		if (leftBeatMovers.Count > 0)
		{
			bool ready = (leftBeatMovers[0].transform.position - beatCenter).sqrMagnitude <= Mathf.Pow(beatMoverOffset * beatTolerance, 2);
			return ready;
		}
		return false;
	}

	public void UpdateAggressiveBeats(bool pOneActed, bool pTwoActed)
	{
		// Reset aggressive beat counts when missing a beat, if desired.
		if (resetAggressiveOnMiss)
		{
			if (!pOneActed)
			{
				attackManager.pOneAggressiveBeats = 0;
			}
			if (!pTwoActed)
			{
				attackManager.pTwoAggressiveBeats = 0;
			}
		}

		// Reset aggressive beat counts when players on are on their own sides, if desired.
		if (resetAggressiveOnBack)
		{
			if (playerOne.transform.position.x < 0)
			{
				attackManager.pOneAggressiveBeats = 0;
			}
			if (playerTwo.transform.position.x > 0)
			{
				attackManager.pTwoAggressiveBeats = 0;
			}
		}

		// Check if either character is on the other's side of the mid-line.
		if (playerOne.transform.position.x > 0)
		{
			attackManager.pOneAggressiveBeats++;
		}
		if (playerTwo.transform.position.x < 0)
		{
			attackManager.pTwoAggressiveBeats++;
		}
	}
}
