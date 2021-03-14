using UnityEngine;
using UnityEngine.UI;

public class ChatInputField : MonoBehaviour {
    // Start is called before the first frame update
    private InputField _input_field;

    private void Start(){
        _input_field = GetComponent<InputField>();
    }

    public void ValueChanged(){
        if(_input_field.text.Contains("\n")) ChatManager.instance.WriteMessage(_input_field);
    }
}

