using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Network;
using UnityEngine;

using SkillBridge.Message;
using Common.Data;

namespace Services
{
    class UserService : Singleton<UserService>, IDisposable
    {

        public UnityEngine.Events.UnityAction<Result, string> OnRegister;
        public UnityEngine.Events.UnityAction<Result, string> OnLogin;
        public UnityEngine.Events.UnityAction<Result, string> OnCreateCharacter;

        NetMessage pendingMessage = null;

        bool connected = false;

        bool isQuitGame = false;

        public UserService()
        {
            NetClient.Instance.OnConnect += OnGameServerConnect;
            NetClient.Instance.OnDisconnect += OnGameServerDisconnect;
            //监听用户注册响应，在服务器注册完成后，服务器向客户端发送的消息
            MessageDistributer.Instance.Subscribe<UserCreateCharacterResponse>(this.OnUserCreateCharacter);
            MessageDistributer.Instance.Subscribe<UserLoginResponse>(this.OnUserLogin);
            MessageDistributer.Instance.Subscribe<UserRegisterResponse>(this.OnUserRegister);


        }

        public void Dispose()
        {
            //取消用户注册响应监听
            MessageDistributer.Instance.Unsubscribe<UserCreateCharacterResponse>(this.OnUserCreateCharacter);
            MessageDistributer.Instance.Unsubscribe<UserLoginResponse>(this.OnUserLogin);
            MessageDistributer.Instance.Unsubscribe<UserRegisterResponse>(this.OnUserRegister);

            NetClient.Instance.OnConnect -= OnGameServerConnect;
            NetClient.Instance.OnDisconnect -= OnGameServerDisconnect;
        }

        public void Init()
        {

        }

        public void ConnectToServer()
        {
            Debug.Log("ConnectToServer() Start ");
            //NetClient.Instance.CryptKey = this.SessionId;
            NetClient.Instance.Init("127.0.0.1", 8000);
            NetClient.Instance.Connect();
        }

        /// <summary>
        /// 在与服务器建立链接时执行
        /// </summary>
        /// <param name="result"></param>
        /// <param name="reason"></param>
        void OnGameServerConnect(int result, string reason)
        {
            Log.InfoFormat("LoadingMesager::OnGameServerConnect :{0} reason:{1}", result, reason);
            if (NetClient.Instance.Connected)
            {
                this.connected = true;
                if(this.pendingMessage!=null)
                {
                    NetClient.Instance.SendMessage(this.pendingMessage);
                    this.pendingMessage = null;
                }
            }
            else
            {
                if (!this.DisconnectNotify(result, reason))
                {
                    MessageBox.Show(string.Format("网络错误，无法连接到服务器！\n RESULT:{0} ERROR:{1}", result, reason), "错误", MessageBoxType.Error);
                }
            }
        }

        /// <summary>
        /// 在与服务器断开链接时执行
        /// </summary>
        /// <param name="result"></param>
        /// <param name="reason"></param>
        public void OnGameServerDisconnect(int result, string reason)
        {
            this.DisconnectNotify(result, reason);
            return;
        }
        bool DisconnectNotify(int result,string reason)
        {
            if (this.pendingMessage != null)
            {
                if(this.pendingMessage.Request.userRegister!=null)
                {
                    if (this.OnRegister != null)
                    {
                        this.OnRegister(Result.Failed, string.Format("服务器断开！\n RESULT:{0} ERROR:{1}", result, reason));
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 封装注册消息发送给服务器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="psw"></param>
        public void SendRegister(string user, string psw)
        {
            Debug.LogFormat("UserRegisterRequest::user :{0} psw:{1}", user, psw);
            NetMessage message = new NetMessage();
            message.Request = new NetMessageRequest();
            message.Request.userRegister = new UserRegisterRequest();
            message.Request.userRegister.User = user;
            message.Request.userRegister.Passward = psw;

            if (this.connected && NetClient.Instance.Connected)
            {
                this.pendingMessage = null;
                NetClient.Instance.SendMessage(message);
            }
            else
            {
                this.pendingMessage = message;
                this.ConnectToServer();
            }
        }

        /// <summary>
        /// 服务器注册响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="response"></param>
        void OnUserRegister(object sender, UserRegisterResponse response)
        {
            Debug.LogFormat("OnUserRegister:{0} [{1}]", response.Result, response.Errormsg);

            if (this.OnRegister != null)
            {
                this.OnRegister(response.Result, response.Errormsg);

            }
        }

        /// <summary>
        /// 封装登录消息并发送消息给服务器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="psw"></param>
        public void SendLogin(string user, string psw)
        {
            Debug.LogFormat("UserLoginRequest::user :{0} psw :{1}", user, psw);
            NetMessage message = new NetMessage();
            message.Request = new NetMessageRequest();
            message.Request.userLogin = new UserLoginRequest();
            message.Request.userLogin.User = user;
            message.Request.userLogin.Passward = psw;

            if(this.connected && NetClient.Instance.Connected)
            {
                this.pendingMessage = null;
                NetClient.Instance.SendMessage(message);
            }
            else
            {
                this.pendingMessage = message;
                this.ConnectToServer();
            }
        }

        /// <summary>
        /// 服务器登录响应，接收服务器登录消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="response"></param>
        void OnUserLogin(object sender, UserLoginResponse response)
        {
            Debug.LogFormat("OnUserLogin: {0} Errormsg:[{1}] Id:[{2}] Characters.Count:[{3}]", response.Result, response.Errormsg, response.Userinfo.Player.Id, response.Userinfo.Player.Characters.Count);

            if (response.Result == Result.Success)
            {
                Models.User.Instance.SetupUserInfo(response.Userinfo);
            }

            if (this.OnLogin != null)
            {
                this.OnLogin(response.Result, response.Errormsg);
            }
        }

        public void SendCharacterCreate(string charName, CharacterClass charClass)
        {
            Debug.LogFormat("UserCharacterClass:: Name:{0} Class{1}", charName, charClass);

            NetMessage message = new NetMessage();
            message.Request = new NetMessageRequest();
            message.Request.createChar = new UserCreateCharacterRequest();
            message.Request.createChar.Name = charName;
            message.Request.createChar.Class = charClass;

            if (this.connected && NetClient.Instance.Connected)
            {
                this.pendingMessage = null;
                NetClient.Instance.SendMessage(message);
            }
            else
            {
                this.pendingMessage = message;
                this.ConnectToServer();
            }
        }

        void OnUserCreateCharacter(object sender, UserCreateCharacterResponse response)
        {
            Debug.LogFormat("OnUserCreateCharacter: {0} Errormsg:[{1}] ", response.Result, response.Errormsg);

            if (this.OnLogin != null)
            {
                this.OnLogin(response.Result, response.Errormsg);
            }
        }
    }
}
