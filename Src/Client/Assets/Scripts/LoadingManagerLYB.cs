using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using SkillBridge.Message;
using ProtoBuf;
using Services;
//using Managers;
using System;

public class LoadingManagerLYB : MonoBehaviour
{

    public GameObject UIStart;
    public GameObject UITips;
    public GameObject UILoading;
    public GameObject UILogin;

    public Slider progressBar;
    public Text progressText;
    public Text progressNumber;

    // Use this for initialization
    IEnumerator Start()
    {
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.xml"));
        UnityLogger.Init();
        Common.Log.Init("Unity");
        Common.Log.Info("LoadingManager start");

        //开始界面
        UIStart.SetActive(true);
        UITips.SetActive(false);
        UILoading.SetActive(false);
        UILogin.SetActive(false);
        //yield return new WaitForSeconds(2f);
        //健康游戏提示
        UIStart.SetActive(false);
        UITips.SetActive(true);

        //yield return new WaitForSeconds(2f);
        //资源加载界面
        UILoading.SetActive(true);
        yield return new WaitForSeconds(1f);
        UITips.SetActive(false);

        yield return DataManager.Instance.LoadData();

        UserService.Instance.Init();

        //yield return DataManager.Instance.LoadData();


        // Fake Loading Simulate
        //for (float i = 0; i < 100;)
        //{
        //    i += UnityEngine.Random.Range(0.1f, 0.5f);
        //    progressBar.value = i;
        //    progressNumber.text = Convert.ToInt32(i).ToString() + "%";
        //    yield return new WaitForEndOfFrame();
        //}

        UILoading.SetActive(false);
        UILogin.SetActive(true);
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {

    }
}

