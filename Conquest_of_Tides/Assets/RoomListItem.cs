using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    public Text text;
    RoomInfo info;
    public void Setup(RoomInfo roominfo)
    {
        info = roominfo;
        text.text = roominfo.Name;
    }
    public void OnClick()
    {
        Launcher.instance.JoinRoom(info);
        Networking_UI.instance.RoomMenu();
    }
}
