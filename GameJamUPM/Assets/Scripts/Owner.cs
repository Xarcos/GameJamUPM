using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour {

	[SerializeField] UnityEngine.UI.Image m_faceGesture;
    [SerializeField] UnityEngine.UI.Image m_handsGesture;

    public void MakeGesture(Actions action)
    {
        var gestureSprites = MgrOwnerGestures.getGesture(action);
        m_handsGesture.sprite = gestureSprites.hands;
        m_faceGesture.sprite = gestureSprites.face;
    }
}
