using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public string dbIp = "127.0.0.1";
    public int Port = 3306;
    private string dbName = "game";
    private string tableName = "users";
    private string rootPasswd = "1234"; 
    private MySqlConnection conn; //mysql(mariadb)DB�� ���� ���¸� �����ϴ� ��ü.

    public static DatabaseManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DBConnect();
    }
    //�����ͺ��̽��� ����
    private async void DBConnect()
    {
        string config = $"server={dbIp};port={Port};database={dbName};" +
            $"uid=root;pwd={rootPasswd};charset=utf8";

        conn = new MySqlConnection(config);
        print($"mysql ���� ����. state : {conn.State}");
        await conn.OpenAsync();
        print($"mysql ���� ����. state : {conn.State}");
    }

    public async void SignUp(string email,string userName,string passwd)
    {
        StringBuilder pwhash = new StringBuilder(); // ��й�ȣ�� �ؽ� Ű�� ������ stringbuilder
        using (SHA256 sHA256 = SHA256.Create())
        {//SHA256 �ؽ� �˰����� ����� ��й�ȣ�� �ؽ�Ű�� ����
            byte[] hashArray = sHA256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
            foreach (byte b in hashArray)
            {
                pwhash.Append($"{b:X2}");
                //pwhash.Append(b.ToString("X2"));
            }
        }
        using (MySqlCommand cmd = new MySqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText =
                $"INSERT INTO {tableName} VALUES ('{email}','{pwhash}','{userName}','�ʺ���','1')";
            int rowsAffected = 0;
            try
            {
                rowsAffected = await cmd.ExecuteNonQueryAsync();

            }
            finally 
            {
                if (rowsAffected > 0)//ȸ������ �Ϸ�
                {
                    UIManager.Instance.PageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("�˸�", "ȸ�����Կ� �����߽��ϴ�.",
                        () => { UIManager.Instance.PageOpen("LogIn"); });
                }
                else//���� ����
                {
                    UIManager.Instance.PageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("�˸�", "ȸ�����Կ� �����߽��ϴ�.",
                        () => { UIManager.Instance.PageOpen("LogIn"); });
                }
            }
            
            
                
            
        }
        
    }

    public async void Login(string email, string passwd)
    {
        StringBuilder pwhash = new StringBuilder(); // ��й�ȣ�� �ؽ� Ű�� ������ stringbuilder
        using (SHA256 sHA256 = SHA256.Create())
        {//SHA256 �ؽ� �˰����� ����� ��й�ȣ�� �ؽ�Ű�� ����
            byte[] hashArray = sHA256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
            foreach (byte b in hashArray)
            {
                pwhash.Append($"{b:X2}");
                //pwhash.Append(b.ToString("X2"));
            }
        }
        using (MySqlCommand cmd = new MySqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText =
                $"SELECT email, username, class, level FROM {tableName} WHERE email='{email}' AND pw='{pwhash}';";

            MySqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (reader.Read())// �α��� ����
            {
                //print($"�α��� ����, " +
                //    $"�̸��� : {reader[0]}, " +
                //    $"�̸� : {reader[1]}, " +
                //    $"���� : {reader[2]}," +
                //    $"���� : {reader[3]}");
                UserData userData = new UserData(
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    (int)reader[3]);
                UIManager.Instance.PageOpen("UserInfo");
                UIManager.Instance.userInfo.UserInfoOpen(userData);

            }
            else//�α��� ����
            {
                //print("�α��� ����");
                UIManager.Instance.PageOpen("Popup");
                UIManager.Instance.popup.PopupOpen(
                    "�˸�",
                    "�α��ο� �����߽��ϴ�.",
                    () => UIManager.Instance.PageOpen("LogIn"));
            }

            
        }
    }

    public async void LevelUp(string email)
    {
        
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        try
        {
            await conn.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE {tableName} SET level = level + 1 WHERE email = '{email}'";

                int result = await cmd.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    // ������ ���� �� ���� ���� �ٽ� ��������
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE email = '{email}'";
                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            UserData userData = new UserData(
                                email,
                                reader["username"].ToString(),
                                reader["class"].ToString(),
                                Convert.ToInt32(reader["level"])
                            );
                            UIManager.Instance.userInfo.UserInfoOpen(userData);
                            UIManager.Instance.popup.PopupOpen("�˸�", "������ ����!", null);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"������ ����: {ex.Message}");
            UIManager.Instance.popup.PopupOpen("����", "�������� �����߽��ϴ�.", null);
        }
    }
    public async void DeleteAccount(string email)
    {
        Debug.Log($"Attempting to delete account: {email}");
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        try
        {
            await conn.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM {tableName} WHERE email = '{email}'";
                Debug.Log($"Executing query: {cmd.CommandText}");
                int result = await cmd.ExecuteNonQueryAsync();
                Debug.Log($"Delete result: {result}");
                if (result > 0)
                {
                    UIManager.Instance.popup.PopupOpen("�˸�", "ȸ��Ż�� �Ϸ�Ǿ����ϴ�.", null);
                    UIManager.Instance.PageOpen("LogIn");  // �α��� �������� �̵�
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"ȸ��Ż�� ����: {ex.Message}");
            UIManager.Instance.popup.PopupOpen("����", "ȸ��Ż�� �����߽��ϴ�.", null);
        }
    }
    public async void GetRanking(Action<string> callback)
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }

        try
        {
            await conn.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT username, level, class FROM {tableName} ORDER BY level DESC LIMIT 10";

                using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    string rankingText = "����\t�̸�\tŬ����\t����\n\n";
                    int rank = 1;

                    while (await reader.ReadAsync())
                    {
                        rankingText += $"{rank}��\t";
                        rankingText += $"{reader["username"]}\t";
                        rankingText += $"{reader["class"]}\t";
                        rankingText += $"Lv.{reader["level"]}\n\n";
                        rank++;
                    }

                    // 10���� �� �Ǵ� ��� �� ���� ǥ��
                    while (rank <= 10)
                    {
                        rankingText += $"{rank}��\t---\t---\tLv.0\n\n";
                        rank++;
                    }

                    callback?.Invoke(rankingText);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"��ŷ ��ȸ ����: {ex.Message}");
            UIManager.Instance.popup.PopupOpen("����", "��ŷ ��ȸ�� �����߽��ϴ�.", null);
        }
        finally
        {
            // ���� �ݱ�
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    //������ ��ȭ�� �������� �׽�Ʈ
    public void SelectAll()
    { 
        //����(query)�� ������ command ��ü�� ����
        MySqlCommand cmd = new MySqlCommand();

        cmd.Connection = conn;//������ db�� �Է�.
        cmd.CommandText =
            $"SELECT * FROM {tableName}";

        //���� ��� �����ͼ��� c#���� ����� �� �ִ� ���·� ������.
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd); 
        DataSet set = new DataSet();

        dataAdapter.Fill(set);

        //�����Ͱ� ���������� ��ȭ�� �Ǿ����� ���θ� DataSet�� ���̺� ������ �� ������ ���� Ȯ��
        bool isSelectSucceed = set.Tables.Count > 0 && set.Tables[0].Rows.Count>0;

        if (isSelectSucceed)//��ȸ ����
        {
            print("������ ��ȸ ����");
            foreach (DataRow row in set.Tables[0].Rows)
            {
                print($"�̸��� : {row["email"]},�̸� : {row["username"]}," +
                    $"���� : {row["class"]},���� : {row["level"]}");
            }
        }
        else//��ȸ ����
        {
            print("������ ��ȸ ����");
        }
    }
    
}
