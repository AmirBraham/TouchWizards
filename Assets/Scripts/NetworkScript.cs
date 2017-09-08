using UnityEngine;
using UnityEngine.Networking;

public class NetworkScript : NetworkBehaviour {
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    private const string typeName = "UniqueGameName";
    private const string gameName = "RoomName";
    private void StartServer()
    {
        NetworkServer.Listen(25000);
        Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }
    void OnServerInitialized()
    {
        NetworkServer.Spawn(player1Prefab);
        Network.Instantiate(player1Prefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        Debug.Log("Server Initializied");
    }

    private HostData[] hostList;

    private void RefreshHostList()
    {
        Debug.Log(hostList);
        MasterServer.RequestHostList(typeName);
    }
    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }

    }


    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Debug.Log(msEvent);
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }
    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        NetworkServer.Spawn(player2Prefab);
        Network.Instantiate(player2Prefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        Debug.Log("Server Joined");
    }
}
