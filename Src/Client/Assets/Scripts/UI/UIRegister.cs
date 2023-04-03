using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;

using SkillBridge.Message;

public class UIRegister : MonoBehaviour
{

    //定义变量
    public InputField username;
    public InputField password;
    public InputField passwordConfirm;
    public Button buttonRegister;
    public Button buttonBack;

    public GameObject uiLogin;
    public InputField loginUsername;
    public InputField loginPassword;


    // Use this for initialization
    void Start()
    {
        UserService.Instance.OnRegister += this.OnRegister;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickRegister()
    {
        //账号判断
        if (string.IsNullOrEmpty(this.username.text))
        {
            MessageBox.Show("请输入账号");
            return;
        }

        //密码判断
        if (string.IsNullOrEmpty(this.password.text))
        {
            MessageBox.Show("请输入密码");
            return;
        }

        //密码确认判断
        if (string.IsNullOrEmpty(this.passwordConfirm.text))
        {
            MessageBox.Show("请输入确认密码");
            return;
        }

        //两次密码比较
        if (this.password.text != this.passwordConfirm.text)
        {
            MessageBox.Show("两次输入的密码不一致");
            return;
        }

        //MessageBox.Show("注册成功");
        UserService.Instance.SendRegister(this.username.text, this.password.text);

    }
    void OnRegister(Result result, string msg)
    {
        if (result == Result.Success)
        {
            //登录成功，进入角色选择
            MessageBox.Show("注册成功,请登录", "提示", MessageBoxType.Information).OnYes = this.CloseRegister;
        }
        else
            MessageBox.Show(msg, "错误", MessageBoxType.Error);
    }

    void CloseRegister()
    {
        this.gameObject.SetActive(false);
        uiLogin.SetActive(true);
        loginUsername.text = this.username.text;
        loginPassword.text = this.password.text;
    }
}
