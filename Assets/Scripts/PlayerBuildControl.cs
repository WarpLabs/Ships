using UnityEngine;
using System.Collections;

public class PlayerBuildControl : MonoBehaviour {

	public GameObject Selector;
	public DrawGrid Grid;

	private bool BuildModeOn = false;


	void Start () {

		DisableBuild ();
		
	}

	void Update () {

		if (Input.GetButtonDown("Fire") && BuildModeOn) {



		}

	}

	public void EnableBuild () {

		Selector.SetActive (true);
		Grid.showMain = true;

		BuildModeOn = true;

	}

	public void DisableBuild () {

		Selector.SetActive (false);
		Grid.showMain = false;

		BuildModeOn = false;

	}


}
