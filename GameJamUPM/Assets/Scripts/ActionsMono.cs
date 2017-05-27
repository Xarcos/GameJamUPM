using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsMono : MonoBehaviour {

	[SerializeField] Actions m_ACTION;
    [SerializeField] KeyCode key;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGestureMade += OnGestureMade;
        gameManager.OnActionMade += OnActionMade;
    }

    void Update() {
        if (Input.GetKeyDown(key)) {
            gameManager.MakeAction(m_ACTION);
        }
    }

    public void OnClick_Button()
    {
        gameManager.MakeAction(m_ACTION);
        Debug.Log(m_ACTION.ToString());
    }

    void OnGestureMade()
    {
        Debug.Log("GestureMde");
        GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    void OnActionMade()
    {
        Debug.Log("ActionMade");
        GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
