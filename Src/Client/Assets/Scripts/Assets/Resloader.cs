using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 为自动更新做准备
/// 是一个资源加载类，将来资源更新逻辑会用其扩展
/// </summary>
class Resloader
{
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }
}