using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogAvatar : MonoBehaviour
{

    [System.Serializable]
    public class status
    {
        public Sprite Avatar;
        public int minHp;
    }

    public void loseHp(int loss)
    {
        currentHP = currentHP - loss < 0 ? 0 : currentHP - loss;
        if (currentHP == 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().EndGame();
        }
        updateStateSprite();
    }

    public void gainHp(int gain)
    {
        currentHP = currentHP + gain > MAX_LIFE ? MAX_LIFE : currentHP + gain;
        updateStateSprite();
    }
    public void setBoost(bool newBoost)
    {
        isBoosted = newBoost;
        updateStateSprite();
    }
    void updateStateSprite()
    {
        for (int i = 0; i < dogStates.Length; ++i)
        {
            if (currentHP >= dogStates[i].minHp)
            {
                state = i;
                if (!isBoosted)
                {
                    dogAvatar.sprite = dogStates[state].Avatar;
                }
                else
                {
                    dogAvatar.sprite = boostAvatar;
                }
            }

        }

    }

    [SerializeField]
    status[] dogStates;

    [SerializeField]
    Image dogAvatar;
    [SerializeField]
    Sprite boostAvatar;

    [SerializeField]
    float currentHP = 100;

    public int state;
    bool isBoosted;

    public float MAX_LIFE;
    // Use this for initialization
    void Start()
    {
        updateStateSprite();
    }
}
