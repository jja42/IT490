using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;
    public Text RoomName;
    public Text error;
    public Text Connecting;
    public Text RoomDisplayName;
    public Transform RoomListContent;
    public GameObject RoomInfoPrefab;
    public bool can_interact;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Connecting.text = "Connecting to Server";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Connecting.text = "Connecting to Server...";
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Connecting.text = "Connected to Server";
        can_interact = true;
    }

    // Update is called once per frame
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomName.text))
            return;
        PhotonNetwork.CreateRoom(RoomName.text);
    }

    public override void OnJoinedRoom()
    {
        RoomDisplayName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        error.text = "Room Creation Failed :" + message;
        error.gameObject.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform transform in RoomListContent)
        {
            Destroy(transform.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(RoomInfoPrefab, RoomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }
    public void LoadMainScene()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
