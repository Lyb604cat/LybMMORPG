using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharaterView : MonoBehaviour
{
    public GameObject[] charaters;

    private int currentCharacter = 0;
    public int CurrentCharacter
    {
        get
        {
            return currentCharacter;
        }
        set
        {
            currentCharacter = value;
            this.UpdateCharacter();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void UpdateCharacter()
    {
        for (int i = 0; i < charaters.Length; i++)
        {
            charaters[i].SetActive(i == this.currentCharacter);
        }
    }

}
