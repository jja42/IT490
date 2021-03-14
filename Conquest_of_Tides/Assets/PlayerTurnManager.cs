using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerTurnManager : MonoBehaviour
{
    PhotonView PV;
    public static PlayerTurnManager instance;
    public int turn_id;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (PV.IsMine)
        {
            instance = this;
            turn_id = PV.ControllerActorNr;
            GameManager.instance.Pause();
            General_UI_Manager.instance.SetPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AttachFortificationActive(int fort_id)
    {
        PV.RPC("AttachActive", RpcTarget.Others, fort_id);
    }
    public void AttachFortificationBench(int fort_id, int index)
    {
        PV.RPC("AttachBench", RpcTarget.Others, fort_id, index);
    }
    public void SetBench(int id)
    {
        PV.RPC("SetOpponentBench", RpcTarget.Others,id);
    }
    public void SetActive(int id)
    {
        PV.RPC("SetOpponentActive", RpcTarget.Others,id);
    }
    public void Switch()
    {
        PV.RPC("SwitchPlayer", RpcTarget.Others);
    }
    public void Pause()
    {
        PV.RPC("PausePlayer", RpcTarget.Others);
    }
    public void SetTurnPhase(int phase)
    {
        PV.RPC("SetPhase", RpcTarget.Others, phase);
    }
    public void RepositionShips(int index)
    {
        PV.RPC("Reposition", RpcTarget.Others, index);
    }
    public void Combat(int damage)
    {
        PV.RPC("Attack", RpcTarget.Others, damage);
    }
    public void Match (string username)
    {
        PV.RPC("MatchLog", RpcTarget.Others, username);
    }
    [PunRPC]
    public void MatchLog(string opponent_name)
    {
        StartCoroutine(WebRequest.instance.MatchHistory(Settings_Manager.instance.username, opponent_name));
    }
    [PunRPC]
    public void Reposition(int index)
    {
        General_UI_Manager.instance.OpponentReposition(index);
    }
    [PunRPC]
    public void SwitchPlayer()
    {
        if (Turn_Manager.instance.currPlayer == Turn_Manager.TurnPlayer.Player1)
        {
            Turn_Manager.instance.currPlayer = Turn_Manager.TurnPlayer.Player2;
            GameManager.instance.Pause();
            General_UI_Manager.instance.SetPlayer();
            return;
        }
        if (Turn_Manager.instance.currPlayer == Turn_Manager.TurnPlayer.Player2)
        {
            Turn_Manager.instance.currPlayer = Turn_Manager.TurnPlayer.Player1;
            GameManager.instance.Pause();
            General_UI_Manager.instance.SetPlayer();
            return;
        }
    }
    [PunRPC]
    public void AttachActive(int fort_id)
    {
        General_UI_Manager.instance.OpponentAttachFortificationActive(fort_id);
    }
    [PunRPC]
    public void AttachBench(int fort_id, int index)
    {
        General_UI_Manager.instance.OpponentAttachFortificationBench(fort_id,index);
    }
    [PunRPC]
    public void SetPhase(int phase)
    {
        Turn_Manager.instance.SetState(phase);
    }
    [PunRPC]
    public void SetOpponentBench (int id)
    {
        GameManager.instance.SetOpponentBench(id);
    }
    [PunRPC]
    public void SetOpponentActive(int id)
    {
        GameManager.instance.SetOpponentActiveZone(id);
    }
    [PunRPC]
    public void Attack(int damage)
    {
        Combat_Manager.instance.HandleOpponentCombat(damage);
    }
}
