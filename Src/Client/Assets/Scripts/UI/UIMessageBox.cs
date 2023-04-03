using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIMessageBox : MonoBehaviour {

    public Text title;
    public Text message;
    public Image[] icons;
    public Button buttonYes;
    public Button buttonNo;
    public Button buttonClose;

    public Text buttonYesTitle;
    public Text buttonNoTitle;

    public UnityAction OnYes;
    public UnityAction OnNo;

    void Start()
    {

    }

    void Update()
    {

    }


    public void Init(string title , string message , MessageBoxType type = MessageBoxType.Information, string btnOk = "", string btnCancel = "")
    {
        if (!string.IsNullOrEmpty(title)) this.title.text = title;
        this.message.text = message;
        this.icons[0].enabled = type == MessageBoxType.Information;
        this.icons[1].enabled = type == MessageBoxType.Confirm;
        this.icons[2].enabled = type == MessageBoxType.Error;

        if(!string.IsNullOrEmpty(btnOk)) this.buttonYesTitle.text = btnOk;
        if(!string.IsNullOrEmpty(btnCancel)) this.buttonNoTitle.text = btnCancel;

        //监听yes和no的按钮
        this.buttonYes.onClick.AddListener(OnClickYes);
        this.buttonNo.onClick.AddListener(OnClickNo);

        ///这里不理解
        this.buttonNo.gameObject.SetActive(type == MessageBoxType.Confirm);

    }

    void OnClickYes()
    {
        Destroy(this.gameObject);
        if(this.OnYes != null)
            this.OnYes();
    }

    void OnClickNo()
    {
        Destroy (this.gameObject);
        if(this.OnNo != null)
            this.OnNo();
    }
}
