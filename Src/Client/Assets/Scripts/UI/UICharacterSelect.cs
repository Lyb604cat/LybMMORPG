﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillBridge.Message;
using Models;
using Services;
using Common;
using Common.Data;

public class UICharacterSelect : MonoBehaviour {

	public GameObject panelCreate;
	public GameObject panelSelect;

	public GameObject btnCreateCharacter;
	//public GameObject btnCreateCancel;


	public Transform uiCharList;
	public GameObject uiCharInfo;

	public List<GameObject> uiChars = new List<GameObject>();

	public Image[] imageClassTitle;
	public Image[] imageSelectClassButton;
	public Text descs;

	public InputField charName;//角色名称
	private CharacterClass charClass;//角色职业
	public Sprite[] spriteSelectClassButton;
	public Sprite[] spriteMarkSelectClassButton;

	private int selectCharacterIdx = -1;

	public UICharaterView characterView;


	// Use this for initialization
	void Start () {
		UserService.Instance.Init();
		DataManager.Instance.Load();
        InitCharacterCreate();
		//InitCharacterSelect(true);
		OnSelectClass(0);

		//UserService.Instance.OnCreateCharacter += this.OnCharacterCreate;

		//监听createclass Button
		btnCreateCharacter.GetComponent<Button>().onClick.AddListener(() =>
		{
			OnClickCreate();

        });
	}

	public void InitCharacterSelect(bool init)
    {
		panelCreate.SetActive(false);
		panelSelect.SetActive(true);

  //      if (init)
  //      {
		//	foreach(var old in uiChars)
  //          {
		//		Destroy(old);
  //          }
  //      }
		//uiChars.Clear();

		//for(int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
  //      {
		//	GameObject go = Instantiate(uiCharInfo, this.uiCharList);
		//	UICharInfo chrInfo = go.GetComponent<UICharInfo>();
		//	chrInfo.info = User.Instance.Info.Player.Characters[i];

		//	Button button = go.GetComponent<Button>();
		//	int idx = i;
		//	button.onClick.AddListener(() =>
		//	{
		//		OnSelectCharacter(idx);
		//	});

		//	uiChars.Add(go);
		//	go.SetActive(true);
  //      }
    }

	public void InitCharacterCreate()
    {
		panelCreate.SetActive(true);
		panelSelect.SetActive(false);
    }

	// Update is called once per frame
	void Update()
	{

	}

	public void OnClickCreate()
    {
		//TODO: Chreate a new character.\
		if (string.IsNullOrEmpty(this.charName.text))
		{
			MessageBox.Show("请输入角色名称");
			return;
		}

		UserService.Instance.SendCharacterCreate(this.charName.text,this.charClass);
	}

	public void OnSelectClass(int charClass)
    {
		this.charClass = (CharacterClass)charClass;

		characterView.CurrentCharacter = charClass ;

		for (int i = 0; i < 3; i++)
		{
			imageClassTitle[i].gameObject.SetActive(i == charClass );

			if (i == charClass)
			{
				imageSelectClassButton[i].GetComponent<Image>().sprite = spriteMarkSelectClassButton[i];
			}
			else
			{
				imageSelectClassButton[i].GetComponent<Image>().sprite = spriteSelectClassButton[i];
			}

			descs.text = DataManager.Instance.Characters[charClass +1].Description;
		}

	}

	void OnCharacterCreate(Result result, string message)
    {
		if(result == Result.Success)
        {
			InitCharacterSelect(true);
        }
        else
        {
			MessageBox.Show(message,"错误",MessageBoxType.Error);	
        }
    }


	public void OnSelectCharacter(int idx)
	{
		this.selectCharacterIdx = idx;
		var cha = User.Instance.Info.Player.Characters[idx];
		Debug.LogFormat("select char:[{0} {1} {2}]", cha.Id,cha.Name,cha.Class);
		User.Instance.CurrentCharacter = cha;
		characterView.CurrentCharacter = idx;
	}

	public void OnClickPlay()
    {
		if(selectCharacterIdx >= 0)
        {
			MessageBox.Show("进入游戏","进入游戏",MessageBoxType.Confirm);
        }
    }

}
