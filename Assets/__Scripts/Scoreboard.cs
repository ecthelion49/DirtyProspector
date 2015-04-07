using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The scoreboard class manages showing the score to the player
public class Scoreboard : MonoBehaviour {
	public static Scoreboard S;	// The singleton for Scoreboard

	public GameObject	prefabFloatingScore;

	public bool 		_____________________________;

	[SerializeField]
	private int			_score = 0;
	public string		_scoreString;

	// The score property also set the scoreString
	public int score {
		get {
			return(_score);
		}
		set {
			_score = value;
			_scoreString = Utils.AddCommasToNumber (_score);
		}
	}

	// Sthe scoreString property also sets the GUITect.text
	public string scoreString {
		get {
			return(_scoreString);
		}
		set {
			_scoreString = value;
			GetComponent<GUIText> ().text = _scoreString;
		}
	}

	void Awake() {
		S = this;
	}

	// When called by SendMessage, this adds the fs.score to this.score
	public void FSCallback (FloatingScore fs) {
		score += fs.score;
	}

	// This will instantiate a new FloatingScore GameObject and initialize it
	// it also returns a pointer to the FloatingScore created so that the
	// calling function can do more with it (like set fontSizes etc)
	public FloatingScore CreateFloatingScore(int amt, List<Vector3> pts) {
		GameObject go = Instantiate (prefabFloatingScore) as GameObject;
		FloatingScore fs = go.GetComponent<FloatingScore> ();
		fs.score = amt;
		fs.reportFinishTo = this.gameObject;	// Set fs to call back to this
		fs.Init (pts);
		return (fs);
	}


}
