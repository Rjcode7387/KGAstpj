using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    public Button connect;
    public RectTransform textArea;
    public TextMeshProUGUI textPrefab;
    private string ipAddress = "127.0.0.1";
    private int port = 9999;


    private bool isConnected = false;
    private Thread serverMainThread;
    private int clientId = 0;
    private List<ClientHandler>clients = new List<ClientHandler>();
    public static Queue<string> log = new Queue<string>();

    private void Awake()
    {
        connect.onClick.AddListener(ConnectButtonClick);
    }

    private void Update()
    {
        if (log.Count > 0)
        {
            TextMeshProUGUI logText = Instantiate(textPrefab, textArea);
            logText.text = log.Dequeue();
        }
    }


    private void ConnectButtonClick()
    {
        if (false == isConnected)
        {
            serverMainThread = new Thread(ServerThread);
            serverMainThread.IsBackground = true;
            serverMainThread.Start();
            isConnected = true;
        }
        else 
        {
            serverMainThread.Abort();
            isConnected = false;
        }
    }
    private void ServerThread()
    {
        try
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            tcpListener.Start();
            log.Enqueue("서버 시작됨");
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient(); //TCP Ŭ���̾�Ʈ�� ������ �� ���� ���.
                ClientHandler handler = new ClientHandler();
                handler.Connect(clientId++, this, tcpClient);
                clients.Add(handler);
                log.Enqueue($"{clientId}번 클라이언트가 접속됨");
            }
        }
        catch
        {
            log.Enqueue("무서운 일이 버러지고있어");
        }
        finally
        {
            foreach (ClientHandler client in clients)
            {
                client.Disconnect();
            }
            serverMainThread.Abort();
            isConnected = false;
        }
    }

    public void Disconnect(ClientHandler client)
    {
        clients.Remove(client);
    }
    public void BroadcastToClients(string message)
    {
        log.Enqueue(message);
        foreach (ClientHandler client in clients)
        {
            client.MessageToClient(message);
        }
    }

   
}

public class ClientHandler
{
    public int id;
    public Server server;
    public TcpClient tcpClient;
    public Thread clientThread;
    public StreamReader reader;
    public StreamWriter writer;

    public void Connect(int id, Server server, TcpClient tcpClient)
    {
        this.id = id;
        this.server = server;
        this.tcpClient = tcpClient;
        reader = new StreamReader(tcpClient.GetStream());
        writer = new StreamWriter(tcpClient.GetStream());
        writer.AutoFlush = true;
        clientThread = new Thread(Run);
        clientThread.IsBackground = true;
        clientThread.Start();
    }


    public void Disconnect()
    {
        clientThread.Abort();
        writer.Close();
        reader.Close();
        tcpClient.Close();
        server.Disconnect(this);
    }

    public void MessageToClient(string message)
    {
        writer.WriteLine(message);
    }

    public void Run()
    {
        try
        {
            while (tcpClient.Connected)
            {
                string receiveMessage = reader.ReadLine();
                if (string.IsNullOrEmpty(receiveMessage))
                {
                    continue;
                }
                ClickData data = JsonUtility.FromJson<ClickData>(receiveMessage);
                data.clientId = id;
                server.BroadcastToClients(JsonUtility.ToJson(data));

            }
        }
        finally
        {
            Server.log.Enqueue($"{id}번 클라이언트 오류로 종료됨.");
        }

    }



}