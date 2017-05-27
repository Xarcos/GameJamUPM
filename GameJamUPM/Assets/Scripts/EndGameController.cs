using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.InputField m_inputField;

	// Use this for initialization
	void Start () {
        m_scoreText.text = "Tu puntuación:\n" + ScoreManager.SCORE.ToString();
	}

    public void OnContinue_ButtonClick()
    {
        LeaderBoardManager.saveNewScore(ScoreManager.SCORE, m_inputField.text);
        SceneManager.LoadScene("mainmenu");
    }
}
