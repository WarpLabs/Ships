using UnityEngine;
using System.Collections;

public class PlayerBuildControl : MonoBehaviour {

	public GameObject Selector;
	//public DrawGrid Grid;
	public GameObject Ship;

	public GameObject[] Blocks;

	public LayerMask ScanMask;

	private bool BuildModeOn = false;
	private int CurrentBlock = 0;

	private bool FirstBlock = true;

	private GameObject ReadyBlock;

	void Update () {

		if (ReadyBlock != null) {

			ReadyBlock.transform.position = Selector.transform.position;
			ReadyBlock.transform.rotation = Selector.transform.rotation;

		}

	}

	void Start () {

		DisableBuild ();

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

				if (Input.GetButtonDown ("SwitchForward") || Input.GetButtonDown ("SwitchBackward")) {

					if (Input.GetButtonDown ("SwitchForward") && CurrentBlock < (Blocks.Length - 1))
						CurrentBlock++;
					else if (Input.GetButtonDown ("SwitchBackward") && CurrentBlock > 0)
						CurrentBlock--;

					rend = Blocks [CurrentBlock].GetComponent<SpriteRenderer> ();
					blockRend.sprite = rend.sprite;

				}

				if (Input.GetButtonDown ("Fire") && BuildModeOn) {

					string AttemptedBlock = Blocks [CurrentBlock].tag;

					Collider2D[] objsInArea = Physics2D.OverlapBoxAll (Selector.transform.position, new Vector2 (0.5f, 0.5f), 0f, ScanMask.value);

					if (AttemptedBlock == "Base" && objsInArea.Length <= 0) {

						Vector3 noZPos = new Vector3 (Selector.transform.position.x, Selector.transform.position.y, 0.1f);

						if (FirstBlock) {
							Ship.transform.position = noZPos;
							FirstBlock = false;
						}

						GameObject block = Instantiate (Blocks [CurrentBlock], noZPos, Selector.transform.rotation) as GameObject;
						block.transform.parent = Ship.transform;
						break;

					}
					else if (AttemptedBlock != "Base" && objsInArea.Length == 1) {

						Collider2D objInArea = Physics2D.OverlapBox (Selector.transform.position, new Vector2 (0.5f, 0.5f), 0f, ScanMask.value);

						if (objInArea.gameObject.tag == "Base") {

							Vector3 noZPos = new Vector3 (Selector.transform.position.x, Selector.transform.position.y, 0);

							if (FirstBlock) {
								Ship.transform.position = noZPos;
								FirstBlock = false;
							}

							GameObject block = Instantiate (Blocks [CurrentBlock], noZPos, Selector.transform.rotation) as GameObject;
							block.transform.parent = Ship.transform;
							break;

						}

					}

				}

				if (BuildModeOn == false) {
					break;
				}

				//yield return null;

			}

		}

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
