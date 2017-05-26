using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions {Bark, MakeDeath, Paw, Sit }

[System.Serializable]
public class LvlDef
{
    public double puntuationRequired;
    public float tempoTime;
    public float life_on_fail;
    public float life_on_success;
    public double successScore;
}

public class GameManager : MonoBehaviour {

    double m_score = 0;
    float m_life;
    float m_temporizer = 0;

    float m_answerTime;

    double Score
    {
        get
        {
            return m_score;
        }
        set
        {
            m_score = value;
            m_scoreText.text = m_score.ToString();

            // Subimos de nivel en función de la puntuación
            if ((actualLvlDef < (m_lvlDefs.Length - 1)) && (m_lvlDefs[actualLvlDef + 1].puntuationRequired <= m_score))
            {
                ++actualLvlDef;
                m_lvlText.text = actualLvlDef.ToString();
            }
        }
    }
    float Life
    {
        get
        {
            return m_life;
        }
        set
        {
            m_life = Mathf.Clamp(value,0,MAX_LIFE);
            m_lifeText.text = m_life.ToString();

            // @TODO cambiar las imágenes del avatar

            // Comprobar muerte
            if (m_life == 0)
                EndGame();
        }
    }

    Actions m_actualGesture;
    int actualLvlDef = 0;
    [SerializeField] LvlDef[] m_lvlDefs;

    [SerializeField] UnityEngine.UI.Image m_faceGesture;
    [SerializeField] UnityEngine.UI.Image m_handsGesture;
    [SerializeField] UnityEngine.UI.Text m_lifeText;
    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.Text m_lvlText;
    [SerializeField] UnityEngine.UI.Text m_tempText;

    public float MAX_LIFE;

	// Use this for initialization
	void Start () {
        m_life = MAX_LIFE;
        MakeGesture();
	}
	
	// Update is called once per frame
	void Update () {
        // Temporizador
        m_tempText.text = m_temporizer.ToString();
        if ((m_temporizer -= Time.deltaTime) < 0)
        {
            OnWrongAction();
        }
	}

    public void MakeGesture()
    {
        // @TODO Escoger un gesto
        var values = System.Enum.GetValues(typeof(Actions));
        m_actualGesture = (Actions)values.GetValue(Random.Range(0, values.Length));

        // @TODO Hacer el gesto
        var gestureSprites = MgrOwnerGestures.getGesture(m_actualGesture);
        m_handsGesture.sprite = gestureSprites.hands;
        m_faceGesture.sprite = gestureSprites.face;

        // @TODO Iniciar temporizador
        m_temporizer = m_lvlDefs[actualLvlDef].tempoTime;
    }

    public void MakeAction(string action)
    {
        if (m_actualGesture.ToString() == action)
            OnCorrectAction();
        else
            OnWrongAction();
    }

    public void MakeAction(Actions action)
    {
        // @TODO
        if (m_actualGesture == action)
            OnCorrectAction();
        else
            OnWrongAction();
    }

    public void OnWrongAction()
    {
        // @TODO
        // Reacción del dueño

        // Perder vida
        Life -= m_lvlDefs[actualLvlDef].life_on_fail;

        // Hacer el nuevo gesto
        MakeGesture();
    }

    public void OnCorrectAction()
    { 
        // @TODO
        // Reacción del dueño

        // Ganar vida
        Life += m_lvlDefs[actualLvlDef].life_on_success;

        // Ganar puntuación
        Score += m_lvlDefs[actualLvlDef].successScore;

        // Hacer el nuevo gesto
        MakeGesture();
    }

    void EndGame()
    {
        // @TODO
        enabled = false;
        m_lifeText.text = "YOU DIE!";
    }
}
