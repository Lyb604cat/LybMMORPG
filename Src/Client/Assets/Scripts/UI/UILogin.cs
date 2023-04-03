using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using SkillBridge.Message;


public class UILogin : MonoBehaviour {


	public InputField username;
	public InputField password;
	public Button buttonLogin;
	public Button buttonRegister;
	public Toggle passwordConfirm;
	public Toggle readingConfirm;


	// Use this for initialization
	void Start () {
		UserService.Instance.OnLogin += this.OnLogin;
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickRegister()
    {
		//客户端验证消息
		if(string.IsNullOrEmpty(this.username.text))
        {
			MessageBox.Show("用户名不能为空");
			return;
        }
		if(string.IsNullOrEmpty(this.password.text))
        {
			MessageBox.Show("密码不能为空");
			return;
		}
        if (!this.passwordConfirm.isOn)
        {
            //MessageBox.Show("");
            //return;
        }
        if (!this.readingConfirm.isOn)
        {
			MessageBox.Show("请阅读并同意《用户协议》");
			return;
		}
		//向服务器发送数据
		UserService.Instance.SendLogin(this.username.text, this.password.text);
    }


	void OnLogin(Result result, string msg)
	{
		if(result == Result.Success)
        {
			//MessageBox.Show("登录成功，准备角色选择,当前角色数量"+ Models.User.Instance.Info.Player.Characters.Count, "提示",MessageBoxType.Information);
			//TODO:跳转场景到角色选择界面
			SceneManager.Instance.LoadScene("CharacterSelect");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");


        }
        else
		{
			MessageBox.Show(msg,"错误",MessageBoxType.Error);
		}
	}
}
