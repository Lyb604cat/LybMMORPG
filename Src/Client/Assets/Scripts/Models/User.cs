using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Services;
using SkillBridge.Message;
using UnityEngine;

namespace Models
{
    class User : Singleton<User>
    {
        NUserInfo userInfo;


        public NUserInfo Info
        {
            get { return userInfo; }
        }


        public void SetupUserInfo(NUserInfo info)
        {
            this.userInfo = info;
        }

        public NCharacterInfo CurrentCharacter { get; set; }

    }
}
