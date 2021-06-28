using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.NetworkInformation;
using Ping = System.Net.NetworkInformation.Ping;

/// <summary>
/// 网络状态检测
/// </summary>
public class NetworkDetecter : MonoBehaviour
{
    private bool networkStatus = false;

    private void Start()
    {
        networkStatus = NetworkPing("www.gov.cn");
        AppAction(networkStatus);
    }


    /// <summary>
    /// Ping命令检测网络是否畅通
    /// </summary>
    /// <param name="url">URL地址</param>
    /// <returns>是否ping通</returns>
    private static bool NetworkPing(string url)
    {
        bool isSucceed = true;
        Ping ping = new Ping();
        try
        {
            PingReply pingReply = ping.Send(url);
            if (pingReply != null && pingReply.Status != IPStatus.Success)
            {
                isSucceed = false;
            }

            if (pingReply != null)
            {
                Debug.Log("NetWork: Ping <" + url + ">/Status <" + pingReply.Status + ">");
            }
        }
        catch
        {
            isSucceed = false;
        }
        return isSucceed;
    }

    private static void AppAction(bool networkStatus)
    {
        if (!networkStatus)
        {
            Debug.Log("App: Application will quit now.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        return;
    }
}