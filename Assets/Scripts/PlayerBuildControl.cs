using UnityEngine;
using System.Collections;

public class PlayerBuildControl : MonoBehaviour {

	public GameObject Selector;
	public DrawGrid Grid;

	private bool BuildModeOn;

	void Update () {

		if (Input.GetButtonDown ("Build")) {

			if (BuildModeOn)
				DisableBuild ();
			else
				EnableBuild ();

		}

	}

	void EnableBuild () {

		Selector.SetActive (true);
		Grid.showMain = true;

		BuildModeOn = true;

	}

	void DisableBuild () {

		Selector.SetActive (false);
		Grid.showMain = false;

		BuildModeOn = false;

	}


}
