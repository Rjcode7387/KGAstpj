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
    private MySqlConnection conn; //mysql(mariadb)DB와 연결 상태를 유지하는 객체.

    public static DatabaseManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DBConnect();
    }
    //데이터베이스에 접속
    private async void DBConnect()
    {
        string config = $"server={dbIp};port={Port};database={dbName};" +
            $"uid=root;pwd={rootPasswd};charset=utf8";

        conn = new MySqlConnection(config);
        print($"mysql 접속 시작. state : {conn.State}");
        await conn.OpenAsync();
        print($"mysql 접속 성공. state : {conn.State}");
    }

    public async void SignUp(string email,string userName,string passwd)
    {
        StringBuilder pwhash = new StringBuilder(); // 비밀번호를 해쉬 키로 변경할 stringbuilder
        using (SHA256 sHA256 = SHA256.Create())
        {//SHA256 해쉬 알고리즘을 사용해 비밀번호를 해쉬키로 변경
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
                $"INSERT INTO {tableName} VALUES ('{email}','{pwhash}','{userName}','초보자','1')";
            int rowsAffected = 0;
            try
            {
                rowsAffected = await cmd.ExecuteNonQueryAsync();

            }
            finally 
            {
                if (rowsAffected > 0)//회원가입 완료
                {
                    UIManager.Instance.PageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("알림", "회원가입에 성공했습니다.",
                        () => { UIManager.Instance.PageOpen("LogIn"); });
                }
                else//가입 실패
                {
                    UIManager.Instance.PageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("알림", "회원가입에 실패했습니다.",
                        () => { UIManager.Instance.PageOpen("LogIn"); });
                }
            }
            
            
                
            
        }
        
    }

    public async void Login(string email, string passwd)
    {
        StringBuilder pwhash = new StringBuilder(); // 비밀번호를 해쉬 키로 변경할 stringbuilder
        using (SHA256 sHA256 = SHA256.Create())
        {//SHA256 해쉬 알고리즘을 사용해 비밀번호를 해쉬키로 변경
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

            if (reader.Read())// 로그인 성공
            {
                //print($"로그인 성공, " +
                //    $"이메일 : {reader[0]}, " +
                //    $"이름 : {reader[1]}, " +
                //    $"직업 : {reader[2]}," +
                //    $"레벨 : {reader[3]}");
                UserData userData = new UserData(
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    (int)reader[3]);
                UIManager.Instance.PageOpen("UserInfo");
                UIManager.Instance.userInfo.UserInfoOpen(userData);

            }
            else//로그인 실패
            {
                //print("로그인 실패");
                UIManager.Instance.PageOpen("Popup");
                UIManager.Instance.popup.PopupOpen(
                    "알림",
                    "로그인에 실패했습니다.",
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
                    // 레벨업 성공 후 유저 정보 다시 가져오기
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
                            UIManager.Instance.popup.PopupOpen("알림", "레벨업 성공!", null);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"레벨업 실패: {ex.Message}");
            UIManager.Instance.popup.PopupOpen("오류", "레벨업에 실패했습니다.", null);
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
                    UIManager.Instance.popup.PopupOpen("알림", "회원탈퇴가 완료되었습니다.", null);
                    UIManager.Instance.PageOpen("LogIn");  // 로그인 페이지로 이동
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"회원탈퇴 실패: {ex.Message}");
            UIManager.Instance.popup.PopupOpen("오류", "회원탈퇴에 실패했습니다.", null);
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
                    string rankingText = "순위\t이름\t클래스\t레벨\n\n";
                    int rank = 1;

                    while (await reader.ReadAsync())
                    {
                        rankingText += $"{rank}위\t";
                        rankingText += $"{reader["username"]}\t";
                        rankingText += $"{reader["class"]}\t";
                        rankingText += $"Lv.{reader["level"]}\n\n";
                        rank++;
                    }

                    // 10명이 안 되는 경우 빈 순위 표시
                    while (rank <= 10)
                    {
                        rankingText += $"{rank}위\t---\t---\tLv.0\n\n";
                        rank++;
                    }

                    callback?.Invoke(rankingText);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"랭킹 조회 실패: {ex.Message}");
            UIManager.Instance.popup.PopupOpen("오류", "랭킹 조회에 실패했습니다.", null);
        }
        finally
        {
            // 연결 닫기
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    //데이터 조화가 가능한지 테스트
    public void SelectAll()
    { 
        //질으(query)를 수행할 command 객체를 생성
        MySqlCommand cmd = new MySqlCommand();

        cmd.Connection = conn;//연결한 db를 입력.
        cmd.CommandText =
            $"SELECT * FROM {tableName}";

        //쿼리 결과 데이터셋을 c#에서 사용할 수 있는 형태로 맞춰줌.
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd); 
        DataSet set = new DataSet();

        dataAdapter.Fill(set);

        //데이터가 성공적으로 조화가 되었는지 여부를 DataSet의 테이블 개수와 행 개수를 통해 확인
        bool isSelectSucceed = set.Tables.Count > 0 && set.Tables[0].Rows.Count>0;

        if (isSelectSucceed)//조회 성공
        {
            print("데이터 조회 성공");
            foreach (DataRow row in set.Tables[0].Rows)
            {
                print($"이메일 : {row["email"]},이름 : {row["username"]}," +
                    $"직업 : {row["class"]},레벨 : {row["level"]}");
            }
        }
        else//조회 실패
        {
            print("데이터 조회 실패");
        }
    }
    
}
