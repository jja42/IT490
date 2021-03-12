using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager: MonoBehaviour {
	public static ChatManager instance;

	public Transform chat_content;
	public GameObject chat_message;
    private string username;
	
	private PhotonView PV;

	// Start is called before the first frame update
	private void Awake(){PV = GetComponent<PhotonView>();}

	private void Start(){
		if(PV.IsMine) instance = this;
        chat_content = GameObject.FindGameObjectWithTag("Content").transform;
        username = Settings_Manager.instance.username;
}

	// Update is called once per frame
	// public void SendMessage(string message){PV.RPC("Bruh", RpcTarget.Others);}
	//
	// [PunRPC]
	// public void Bruh(){print("Bruh");}
	

	//private string col = "cyan"; // set to whatever color the sender should have
	public void WriteMessage(InputField sender){
		if(!string.IsNullOrEmpty(sender.text) && sender.text.Trim().Length > 0){
			sender.text = sender.text.Replace("\r", string.Empty).Replace("\n", string.Empty);
			Send_Message(sender.text,username);
			sender.text = string.Empty;
			sender.ActivateInputField();
		}
	}
	
	public void Send_Message(string message,string username){PV.RPC("TransmitMessage", RpcTarget.All, message,username);}
	
	[PunRPC]
	public void TransmitMessage(string message,string username){
		if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(message)){
			// Message or username was empty, so do not do anything
			return;
		}
		
		GameObject new_message = Instantiate(chat_message, chat_content);
		Text content = new_message.GetComponent<Text>();

		content.text = string.Format(content.text, $"{username}", message);
	}
}