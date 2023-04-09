using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillBridge.Message;

public class UICharInfo : MonoBehaviour {


    public NCharacterInfo info;

    public Text charClass;
    public Text charName;
    public Image highlight;

    public bool Selected
    {
        get { return highlight.IsActive(); }
        set
        {
            highlight.gameObject.SetActive(value);
        }
    }

    // Use this for initialization
    void Start () {
		if(info!=null)
        {
            switch (this.info.Class+1)
            {
                case CharacterClass.Warrior:
                    this.charClass.text = this.info.Level+ " 战士";
                    break;

                case CharacterClass.Archer:
                    this.charClass.text = this.info.Level + " 弓箭手";
                    break;

                case CharacterClass.Wizard:
                    this.charClass.text = this.info.Level + " 魔法师";
                    break;
            }
            //this.charClass.text = this.info.Class.ToString();
            this.charName.text = this.info.Name;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
