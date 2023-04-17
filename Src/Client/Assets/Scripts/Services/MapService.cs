using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managers;
using Models;
using Network;
using UnityEngine;
using SkillBridge.Message;
using Common.Data;

namespace Services
{
    class MapService : Singleton<MapService>, IDisposable
    {

        public int CurrentMapId = 0;
        public MapService()
        {
            MessageDistributer.Instance.Subscribe<MapCharacterEnterResponse>(this.OnMapCharacterEnter);
            MessageDistributer.Instance.Subscribe<MapCharacterLeaveResponse>(this.OnMapCharacterLeave);


        }


        public void Dispose()
        {
            MessageDistributer.Instance.Unsubscribe<MapCharacterEnterResponse>(this.OnMapCharacterEnter);
            MessageDistributer.Instance.Unsubscribe<MapCharacterLeaveResponse>(this.OnMapCharacterLeave);
        }

        public void Init()
        {

        }

        private void OnMapCharacterEnter(object sender, MapCharacterEnterResponse response)
        {

            //debug
            Debug.LogFormat("OnMapCharacterEnter: Map:{0} Count:{1}",response.mapId,response.Characters.Count);
            //查看角色是否存在当前地图
            foreach (var character in response.Characters)
            {
                if(User.Instance.CurrentCharacter.Id == character.Id)
                {
                    //当前角色切换地图
                    User.Instance.CurrentCharacter = character;
                }
                //将角色加载到角色管理器中
                CharacterManager.Instance.AddCharacter(character);
            }
            //加载地图
            if(CurrentMapId != response.mapId)
            {
                this.EnterMap(response.mapId); 
                this.CurrentMapId = response.mapId;
            }
        }


        private void OnMapCharacterLeave(object sender, MapCharacterLeaveResponse response)
        {
            throw new NotImplementedException();
        }

        private void EnterMap(int mapId)
        {
            if (DataManager.Instance.Maps.ContainsKey(mapId))
            {
                MapDefine map = DataManager.Instance.Maps[mapId];
                SceneManager.Instance.LoadScene(map.Resource);
            }
            else
            {
                Debug.LogErrorFormat("EnterMap: Map {0} not existed",mapId);
            }
        }
    }
}
