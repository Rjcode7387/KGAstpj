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
   
        private Thread clientThread;//쓰레드
        private StreamReader reader;//스트림리더
        private StreamWriter writer;//스트림라이더

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
                TcpClient tcpClient = new TcpClient(); // 클라이언트 객체생성
                IPAddress serverAddress = IPAddress.Parse(ip.text); //ip 입련란의 텍스트를 ip 주소로 파싱
                                                                    //0~65535 까지의 번호를 씀. ushort으로 쓰면 효율적이겠지만,C#에서 주로 쓰이는 정수 자료형이 int이므로 port 번호는 int로 취급
                int portNum = int.Parse(port.text);

                IPEndPoint endPoint = new IPEndPoint(serverAddress, portNum); // 주소와 int로 된 포트넘버를 받는다.

                tcpClient.Connect(endPoint);//서버로 연결 시도.

                //여기까지 코드가 실행 되었으면 서버에 접속 성고.
                log.Enqueue("서버 접속 성공");

                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream());
                writer.AutoFlush = true; // 밀어넣는다.

                while (tcpClient.Connected)
                {
                    string receiveMessage = reader.ReadLine();
                    ClickData data = JsonUtility.FromJson<ClickData>(receiveMessage);
                    log.Enqueue($"클라이언트 {data.clientId}가 ({data.x},{data.y}클릭)");
                }

            }
            catch (ApplicationException e)
            {
                log.Enqueue("어플리케이션 에외 발생");
                log.Enqueue(e.Message);
            }

            catch (Exception e)
            {
                log.Enqueue("뭔가 문제가 있다요");
                log.Enqueue(e.Message);
            }
            finally 
            {
                //try문 내외 구문이 실행이 됐건 exception에 의해 끊겼건 반드시 호출함
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
                //접속중이 아니므로 접속시도
                clientThread = new Thread(ClientThead);
                clientThread.IsBackground = true;
                clientThread.Start();
                isConnected = true; 
            }
            else
            {
                //접속 끊기
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
