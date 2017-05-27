using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsMono : MonoBehaviour {

	[SerializeField] Actions m_ACTION;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnClick_Button()
    {
        gameManager.MakeAction(m_ACTION);
    }
}
