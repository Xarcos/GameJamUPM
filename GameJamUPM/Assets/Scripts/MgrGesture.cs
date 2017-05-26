using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OwnerGestures
{
    public Sprite hands;
    public Sprite face;
    public AudioClip voice;
}

[CreateAssetMenu]
public class MgrOwnerGestures : ScriptableObject { 

	public OwnerGestures _BarkGestures;
	public OwnerGestures _MakeDeathGestures;
	public OwnerGestures _PawGestures;
	public OwnerGestures _SitGestures;

    static MgrOwnerGestures instance;

    static MgrOwnerGestures Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<MgrOwnerGestures>("gestures");
            return instance;
        }
    }

    public static OwnerGestures getGesture (Actions action)
    {
        switch (action)
        {
            case Actions.Bark:
                return Instance._BarkGestures;
            case Actions.MakeDeath:
                return Instance._MakeDeathGestures;
            case Actions.Paw:
                return Instance._PawGestures;
            case Actions.Sit:
                return Instance._SitGestures;
            default:
                throw new System.Exception(action.ToString() + " do not exist");
        }
    }
}
