using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public PlayerBuildControl BuildControl;
	public PlayerShooting ShootingControl;

	public int Phase;


	// Phase -1 = No Phase;
	// Phase 0 = Building
	// Phase 1 = Shooting

	void Update () {

		if (Input.GetButtonDown ("Cancel") && Phase != -1) {
			Phase = -1;
			SwitchPhase ();
		}

		if (Input.GetButtonDown ("Build") && Phase != 0) {
			Phase = 0;
			SwitchPhase ();
		}

		if (Input.GetButtonDown ("One") && Phase != 1) {
			Phase = 1;
			SwitchPhase ();
		}

	}

	void SwitchPhase () {

		BuildControl.DisableBuild ();
		ShootingControl.DisableShooting ();

		switch (Phase) {

		case -1:
			break;
			
		case 0:
			BuildControl.EnableBuild ();
			break;

		case 1:
			ShootingControl.EnableShooting ();
			break;


		}


	}

}
