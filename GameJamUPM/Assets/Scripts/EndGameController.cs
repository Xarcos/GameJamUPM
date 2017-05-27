using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.InputField m_inputField;

    string score;

	// Use this for initialization
	void Start () {
        score = ScoreManager.SCORE.ToString();
	}

    void Update()
    {
        m_scoreText.text = "Tu puntuación:\n" + ((m_inputField.text == "") ? "Nombre" : m_inputField.text) + " - "  + score ;
    }

    public void OnContinue_ButtonClick()
    {
        LeaderBoardManager.saveNewScore(ScoreManager.SCORE, m_inputField.text);
        SceneManager.LoadScene("mainmenu");
    }
}
