using Entities;
using Managers;
using Models;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    Dictionary<int, GameObject> Characters = new Dictionary<int, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitGameObjects());
        CharacterManager.Instance.OnCharacterEnter += OnCharacterEnter;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCharacterEnter(Character character)
    {
        CreateCharacterObject(character);
    }
    IEnumerator InitGameObjects()
    {
        Debug.LogFormat("当前角色数量为：{0}", CharacterManager.Instance.Characters.Count);
        foreach(var cha in CharacterManager.Instance.Characters.Values)
        {
            CreateCharacterObject(cha);
            yield return null;
        }
    }

    private void CreateCharacterObject(Character character)
    {
        if(!Characters.ContainsKey(character.Info.Id) || Characters[character.Info.Id] == null)
        {
            Object obj = Resloader.Load<Object>(character.Define.Resource);
            if(obj == null)
            {
                Debug.LogErrorFormat("Character[{0}] Resource[{1}] not existed.",character.Define.TID,character.Define.Resource);
                return;
            }

            GameObject go = (GameObject)Instantiate(obj);
            go.name = "Character_" + character.Info.Id + "_" + character.Info.Name;
            go.transform.position = GameObjectTool.LogicToWorld(character.position);
            go.transform.forward = GameObjectTool.LogicToWorld(character.direction);
            Characters[character.Info.Id] = go;
            Debug.LogFormat("Instantiate character success!! id:{0}  name:{1}  character:{2}", character.Info.Id, character.Info.Name, character.Info.Class);
            EntityController ec = go.GetComponent<EntityController>();
            if(ec != null)
            {
                ec.entity = character;
                ec.isPlayer = character.IsPlayer;
            }

            PlayerInputController pc = go.GetComponent<PlayerInputController>();
            if(pc != null)
            {
                if(character.Info.Id == User.Instance.CurrentCharacter.Id)
                {
                    MainPlayerCamera.Instance.player = go;
                    pc.enabled = true;
                    pc.character = character;
                    pc.entityController = ec;
                }
                else
                {
                    pc.enabled = false;
                }
            }
        }
    }
}
