using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
//using ArabicSupport;
public class Chat : MonoBehaviour
{
    public InputField inputfield;
    public GameObject Message;
    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // inputfield.text =ArabicFixer.Fix(inputfield.text);
        if(Input.GetKeyDown(KeyCode.Return)&&inputfield.text!="")
        {
            SendMessage();
        }
        
    }
    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, PlayerPrefs.GetString("ur") + ": " + inputfield.text);
        inputfield.text = "";
    }
    [PunRPC]
    public void GetMessage(string ReciveMessage)
    {
        GameObject M = Instantiate(Message, Vector3.zero,Quaternion.identity, content.transform);
        M.GetComponent<Message>().message.text =ReciveMessage;
    }
}
