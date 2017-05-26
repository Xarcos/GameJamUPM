using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Text m_scoreText;

	// Use this for initialization
	void Start () {
        m_scoreText.text = ScoreManager.SCORE.ToString();
	}
}
