//using ExitGames.Client.Photon;
//using Photon.Chat;
//using Photon.Pun;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour//, IChatClientListener
{
   // ChatClient chatClient;
   /// <summary>
   /// User id 
   /// </summary>
    [SerializeField] private string UserId;
    /// <summary>
    /// Channel name
    /// </summary>
    [SerializeField] private string channelName;

    [SerializeField] private bool OnChatConnected = false;
    /// <summary>
    /// Photon view object
    /// </summary>
    public PhotonView view;
    /// <summary>
    /// CHat UI controller
    /// </summary>
    public ChatUIController chatUIController;

    private void OnEnable()
    {
        view = gameObject.GetComponent<PhotonView>();
#if UNITY_STANDALONE_WIN || MAC
        chatUIController = FindObjectOfType<ChatUIController>();
        if (view.IsMine)
        {
            chatUIController.OnsetChatManager(this);
        }
#endif
    }

    /// <summary>
    /// Send mesage & sender name
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="message"></param>
    public void OnSendMessage(string sender, string message)
    {
        object[] data = {sender, message};
        view.RPC("publicMessageReceived", RpcTarget.AllViaServer , data);
    }

    [PunRPC]
    private void publicMessageReceived(object[] data)
    {
#if UNITY_STANDALONE_WIN || MAC
        object[] val = data;
        string sender = (string) val[0];
        string message = (string) val[1];
        chatUIController.OnMessageReceived(sender, message);
#endif
    }
    /*
        public void SetUserName(string value)
        {
            UserId = value;
        }

        public void SetChannelName(string value)
        {
            channelName = value;
        }

            public void ConnectToChat()
            {
        #if UNITY_STANDALONE_WIN || MAC
                chatClient = new ChatClient(this);
                chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(UserId));
        #endif
            }

        private void Update()
        {

            #if UNITY_STANDALONE_WIN || MAC
                    if(chatClient!=null)
                    chatClient.Service();
            #endif

        }
        */
}
