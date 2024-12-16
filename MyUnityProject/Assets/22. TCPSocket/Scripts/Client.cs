using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;

namespace Myproject
{
    public class Client : MonoBehaviour
    {
        [Header("IP Input")]
        public TMP_InputField ip;
        public TMP_InputField port;
        public Button connect;

        [Header("Message Input")]
        public TMP_InputField message;
        public Button send;

        [Header("Text Area")]
        public RectTransform textArea;
        public TextMeshProUGUI textPrefab;
   
        private Thread clientThread;//������
        private StreamReader reader;//��Ʈ������
        private StreamWriter writer;//��Ʈ�����̴�

        private bool isConnected;

        public static Queue<string> log = new Queue<string>();

        private void Awake()
        {
            connect.onClick.AddListener(ConnectButtonClick);
            send.onClick.AddListener(() =>  SendSubmit(message.text));
            message.onEndEdit.AddListener(SendSubmit);
        }

        private void Update()
        {
            if (log.Count > 0)
            {
                TextMeshProUGUI logText = Instantiate(textPrefab, textArea);
                logText.text = log.Dequeue();
            }
            if (Input.GetMouseButtonDown(0) && isConnected)
            {
                Vector3 pos = Input.mousePosition;
                string json = JsonUtility.ToJson(new ClickData { x = pos.x, y = pos.y });
                SendSubmit(json);
            }
        }

        private void ClientThead()
        {
            try
            {
                TcpClient tcpClient = new TcpClient(); // Ŭ���̾�Ʈ ��ü����
                IPAddress serverAddress = IPAddress.Parse(ip.text); //ip �Էö��� �ؽ�Ʈ�� ip �ּҷ� �Ľ�
                                                                    //0~65535 ������ ��ȣ�� ��. ushort���� ���� ȿ�����̰�����,C#���� �ַ� ���̴� ���� �ڷ����� int�̹Ƿ� port ��ȣ�� int�� ���
                int portNum = int.Parse(port.text);

                IPEndPoint endPoint = new IPEndPoint(serverAddress, portNum); // �ּҿ� int�� �� ��Ʈ�ѹ��� �޴´�.

                tcpClient.Connect(endPoint);//������ ���� �õ�.

                //������� �ڵ尡 ���� �Ǿ����� ������ ���� ����.
                log.Enqueue("���� ���� ����");

                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream());
                writer.AutoFlush = true; // �о�ִ´�.

                while (tcpClient.Connected)
                {
                    string receiveMessage = reader.ReadLine();
                    ClickData data = JsonUtility.FromJson<ClickData>(receiveMessage);
                    log.Enqueue($"Ŭ���̾�Ʈ {data.clientId}�� ({data.x},{data.y}Ŭ��)");
                }

            }
            catch (ApplicationException e)
            {
                log.Enqueue("���ø����̼� ���� �߻�");
                log.Enqueue(e.Message);
            }

            catch (Exception e)
            {
                log.Enqueue("���� ������ �ִٿ�");
                log.Enqueue(e.Message);
            }
            finally 
            {
                //try�� ���� ������ ������ �ư� exception�� ���� ����� �ݵ�� ȣ����
                if(reader != null)reader.Close();
                if(writer != null)writer.Close();
                clientThread.Abort();
                isConnected = false;
            }

        }

        private void ConnectButtonClick()
        {
            if (false == isConnected)
            {
                //�������� �ƴϹǷ� ���ӽõ�
                clientThread = new Thread(ClientThead);
                clientThread.IsBackground = true;
                clientThread.Start();
                isConnected = true; 
            }
            else
            {
                //���� ����
                clientThread.Abort();
                isConnected= false;
            }
        }

        private void SendSubmit(string message)
        {
            writer.WriteLine(message); 
            this.message.text = "";
        }

    }
}
