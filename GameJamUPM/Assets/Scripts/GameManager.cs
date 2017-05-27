using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum Actions {Bark, MakeDeath, Paw, Sit }

[System.Serializable]
public class LvlDef
{
    public long puntuationRequired;
    public float tempoTime;
    public float life_on_fail;
    public float life_on_success;
    public long successScore;
}

[System.Serializable]
public class boost
{
    public int followedSuccesses;
    public float multiplier;
}

public class GameManager : MonoBehaviour {

    long m_score = 0;
    float m_life;
    float m_temporizer = 0;
    int followedSuccesses = 0;

    float m_answerTime;

    long Score
    {
        get
        {
            return m_score;
        }
        set
        {
            m_score = value;
            m_scoreText.text = "Score:\n" + m_score.ToString();

            // Subimos de nivel en función de la puntuación
            if ((actualLvlDef < (m_lvlDefs.Length - 1)) && (m_lvlDefs[actualLvlDef + 1].puntuationRequired <= m_score))
            {
                ++actualLvlDef;
                m_lvlText.text = "Level: " + actualLvlDef.ToString();
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
            //m_lifeText.text = m_life.ToString();

            // @TODO cambiar las imágenes del avatar

            // Comprobar muerte
            if (m_life == 0)
                EndGame();
        }
    }
    int FollowedSuccesses
    {
        get
        {
            return followedSuccesses;
        }
        set
        {
            followedSuccesses = value;
            if (followedSuccesses == 0)
            {
                actualBoost = 0;
                m_boostText.text = "";
            }
            else if ((actualLvlDef == m_lvlDefs.Length -1) && (actualBoost < (m_boost.Length - 1)) && (m_boost[actualBoost + 1].followedSuccesses <= followedSuccesses))
            {
                ++actualBoost;
                m_boostText.text = "x" + m_boost[actualBoost].multiplier.ToString();
            }
        }
    }

    Actions m_actualGesture;

    int actualLvlDef = 0; [SerializeField] LvlDef[] m_lvlDefs;
    int actualBoost = 0; [SerializeField] boost[] m_boost;

    //[SerializeField] UnityEngine.UI.Text m_lifeText;
    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.Text m_lvlText;
    [SerializeField] UnityEngine.UI.Text m_boostText;
    
    [SerializeField] RectTransform m_floatingTransformOrigin;
    [SerializeField] DelayedDestroy m_floatingScore;

    Owner m_owner;

    public float MAX_LIFE;

	// Use this for initialization
	void Start () {
        m_life = MAX_LIFE;

        m_owner = GameObject.FindGameObjectWithTag("Owner").GetComponent<Owner>();

        MakeGesture();
	}
	
	// Update is called once per frame
	void Update () {
        // Temporizador
        if ((m_temporizer -= Time.deltaTime) < 0)
        {
            OnWrongAction();
        }
	}

    public void MakeGesture()
    {
        // Escoger un gesto
        var values = System.Enum.GetValues(typeof(Actions));
        m_actualGesture = (Actions)values.GetValue(Random.Range(0, values.Length));

        // Hacer el gesto
        m_owner.MakeGesture(m_actualGesture);

        // @TODO Iniciar temporizador
        m_temporizer = m_lvlDefs[actualLvlDef].tempoTime;
    }

    public void MakeAction(Actions action)
    {
        if (m_actualGesture == action)
            OnCorrectAction();
        else
            OnWrongAction();
    }

    public void OnWrongAction()
    {
        // @TODO
        // Reacción del dueño

        // Resetear el número de aciertos seguidos
        FollowedSuccesses = 0;

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
        var score = m_lvlDefs[actualLvlDef].successScore * m_boost[actualBoost].multiplier;
        Score += System.Convert.ToInt64(score);
        CreateFloatingScore(score.ToString());

        // Aciertos seguidos
        ++FollowedSuccesses;

        // Hacer el nuevo gesto
        MakeGesture();
    }

    public void EndGame()
    {
        // @TODO

        ScoreManager.SCORE = Score;

        SceneManager.LoadScene("endgame");
    }

    void CreateFloatingScore(string text)
    {
        var instantiated = Instantiate<DelayedDestroy>(m_floatingScore);
        instantiated.SetText(text.ToString());
        instantiated.transform.SetParent(m_floatingTransformOrigin, false);
    }
}
