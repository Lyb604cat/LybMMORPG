using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox 
{
    static Object cacheObject = null;

    public static UIMessageBox Show(string message, string title = "", MessageBoxType type = MessageBoxType.Information, string btnOK = "", string btnCancel = "")
    {
        //将来会在此做一些资源更新，动态加载
        if (cacheObject == null)
        {
            //如果动态加载资源为空的时候，则直接寻找本地文件进行加载
            cacheObject = Resloader.Load<Object>("UI/UIMessageBox");
        }

        GameObject go = GameObject.Instantiate(cacheObject) as GameObject;
        UIMessageBox msgbox = go.GetComponent<UIMessageBox>();
        msgbox.Init(title, message, type, btnOK, btnCancel);

        return msgbox;
    }

}

public enum MessageBoxType
{
    /// <summary>
    /// Information Dialog with OK button 
    /// </summary>
    Information = 1,

    /// <summary>
    /// Confirm Dialog with OK and Cancel buttons
    /// </summary>
    Confirm = 2,

    /// <summary>
    /// Error Dialog with OK button
    /// </summary>
    Error = 3
}
