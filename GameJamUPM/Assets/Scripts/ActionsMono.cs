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
}
