using UnityEngine;
using System.Collections;

public class PlayerBuildControl : MonoBehaviour {

	public GameObject Selector;
	//public DrawGrid Grid;
	public GameObject Ship;
	public GameObject ShipBase;
	public GameObject ShipWalls;
	public GameObject ShipGuns;

	public GameObject[] Blocks;

	public LayerMask ScanMask;

	private bool BuildModeOn = false;
	private int CurrentBlock = 0;

	private bool FirstBlock = true;

	private GameObject ReadyBlock;

	private SelectorMouseControl SelectorMovement;
	private float SelectorRotationDelta;

	void Update () {

		if (ReadyBlock != null) {

			ReadyBlock.transform.position = Selector.transform.position;
			ReadyBlock.transform.rotation = Selector.transform.rotation;

		}

		SelectorMovement.RotationDelta = SelectorRotationDelta;

	}

	void Start () {

		DisableBuild ();

		SelectorMovement = Selector.GetComponent<SelectorMouseControl> ();

		StartCoroutine (BuildCoroutine ());
		
	}

	IEnumerator BuildCoroutine() {

		while (true) {

			if (BuildModeOn == false) {
				yield return null;
				continue;
			}

			SpriteRenderer blockRend = ReadyBlock.GetComponent<SpriteRenderer>();
			SpriteRenderer rend = Blocks[CurrentBlock].GetComponent<SpriteRenderer>();
			blockRend.sprite = rend.sprite;

			while (true) {

				yield return null;

				if (Input.GetAxisRaw ("Switch") != 0) {

					if (Input.GetAxisRaw ("Switch") > 0 && CurrentBlock < (Blocks.Length - 1))
						CurrentBlock++;
					else if (Input.GetAxisRaw ("Switch") < 0 && CurrentBlock > 0)
						CurrentBlock--;

					rend = Blocks [CurrentBlock].GetComponent<SpriteRenderer> ();
					blockRend.sprite = rend.sprite;

				}

				if (Input.GetButtonDown ("Rotate")) {

					float degrees = 0f;

					if (Input.GetAxisRaw ("Rotate") > 0)
						degrees = -90f;
					else if (Input.GetAxisRaw ("Rotate") < 0)
						degrees = 90f;

					SelectorRotationDelta += degrees;

				}

				if (Input.GetButtonDown ("Fire") && BuildModeOn) {

					string AttemptedBlock = LayerMask.LayerToName(Blocks [CurrentBlock].layer);

					Collider2D[] objsInArea = Physics2D.OverlapBoxAll (Selector.transform.position, new Vector2 (0.5f, 0.5f), 0f, ScanMask.value);
					string firstObj = "";
					if (objsInArea.Length>0)
						firstObj = LayerMask.LayerToName (objsInArea [0].gameObject.layer);

					if (AttemptedBlock == "ShipBase" && objsInArea.Length <= 0) {

						CreateBlock (ShipBase.transform, 1f);
						break;

					} else if (AttemptedBlock == "ShipWall" && objsInArea.Length == 1 && firstObj == "ShipBase") {

						CreateBlock (ShipWalls.transform, 0f);
						break;

					} else if (AttemptedBlock == "ShipSystem" && objsInArea.Length == 1 && firstObj == "ShipBase") {

						CreateBlock (Ship.transform, 0f);
						break;

					} else if (AttemptedBlock == "ShipGun" && objsInArea.Length == 0) {

						CreateBlock (ShipGuns.transform, 0f);
						break;

					}

				}

				if (BuildModeOn == false) {
					break;
				}

				//yield return null;

			}

		}

	}

	void CreateBlock (Transform Parent, float zPos) {

		Vector3 noZPos = new Vector3 (Selector.transform.position.x, Selector.transform.position.y, zPos);

		if (FirstBlock) {
			Ship.transform.position = noZPos;
			FirstBlock = false;
		}

		GameObject block = Instantiate (Blocks [CurrentBlock], noZPos, Selector.transform.rotation) as GameObject;
		block.transform.parent = Parent;

	}

	public void EnableBuild () {

		Selector.SetActive (true);
		//Grid.showMain = true;

		BuildModeOn = true;
		ReadyBlock = new GameObject ();
		ReadyBlock.AddComponent<SpriteRenderer> ();

		Time.timeScale = 0;


	}

	public void DisableBuild () {

		Selector.SetActive (false);
		//Grid.showMain = false;

		BuildModeOn = false;
		Destroy (ReadyBlock);

		Time.timeScale = 1;

	}


}
