using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;     //C#의 데이터 테이블 때문에 사용
using MySql.Data;     //MYSQL함수들을 불러오기 위해서 사용
using MySql.Data.MySqlClient;    //클라이언트 기능을사용하기 위해서 사용

public class DBManager : MonoBehaviour
{
    protected static DBManager instance;
    public static DBManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<DBManager>();
                if(obj != null)
                {
                    instance = obj;
                }
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    MySqlConnection sqlconn = null;
    private string sqlDBip = "ip";
    private string sqlDBname = "dbname";
    private string sqlDBid = "dbid";
    private string sqlDBpw = "dbpw";

    public void sqlConnect()
    {
        //DB정보 입력
        string sqlDatabase = "Server=" + sqlDBip + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

        //접속 확인하기
        sqlconn = new MySqlConnection(sqlDatabase);
        sqlconn.Open();
        Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 되면 OPEN이라고 나타남


    }

    public void sqldisConnect()
    {
        sqlconn.Close();
        Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 끊기면 Close가 나타남 
    }

    public void sqlcmdall(string allcmd) //함수를 불러올때 명령어에 대한 String을 인자로 받아옴
    {
        sqlConnect(); //접속

        MySqlCommand dbcmd = new MySqlCommand(allcmd, sqlconn); //명령어를 커맨드에 입력
        dbcmd.ExecuteNonQuery(); //명령어를 SQL에 보냄

        sqldisConnect(); //접속해제
    }

    public DataTable selsql(string sqlcmd)  //리턴 형식을 DataTable로 선언함
    {
        DataTable dt = new DataTable(); //데이터 테이블을 선언함

        sqlConnect();
        MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd, sqlconn);
        adapter.Fill(dt); //데이터 테이블에  채워넣기를함
        sqldisConnect();

        return dt; //데이터 테이블을 리턴함
    }

    public void InputUserSQL(string user_name) //유저 프로필 생성
    {
        sqlcmdall("insert into user_info(user_name) values('" + user_name + "')");

    }

    public void UpdateUserSQL(String user_name, int score) //유저 점수 갱신
    {
        sqlcmdall("update user_info set user_score=" + score + " where user_name=" + "'" + user_name + "'");

    }

    public DataTable GetUserScoreSQL() //유저 점수 String 형태로 가져옴
    {
        DataTable dt = selsql("select user_score from user_info where user_name like 'testuser1'");
        string st = Convert.ToString(dt.Rows[0][0]); //st에 저장

        Debug.Log("유저 점수 : " + st);
        return dt;
    }

    public DataTable GetRankingSQL() //유저 랭킹
    {
        DataTable dt = selsql("SELECT" +
                                    " user_name, user_score," +
                                    " (SELECT COUNT(*) + 1 FROM user_info WHERE user_score > b.user_score) AS rank" +
                                    " FROM" +
                                    " user_info AS b" +
                                    " ORDER BY" +
                                    " rank ASC");

        for (int i = 0; i < 10; i++)
        {


            Debug.Log("유저이름 : " + Convert.ToString(dt.Rows[i][0]) + " 점수 : " + Convert.ToString(dt.Rows[i][1]));

        }


        return dt;
    }
    public string GetRankingSQL(string user_name) //유저 랭킹
    {
        DataTable dt = selsql("SELECT" +
                                    " (SELECT COUNT(*) + 1 FROM user_info WHERE user_score > b.user_score) AS rank" +
                                    " FROM user_info AS b" +
                                    " WHERE user_name like '" + user_name + "'"+
                                    " ORDER BY" +
                                    " rank ASC");

        return Convert.ToString(dt.Rows[0][0]);
    }

    public Boolean IdCheckSQL(String user_name) //유저 닉네임 중복체크
    {
        if (user_name.Equals(""))
            return false;

        try
        {
            DataTable dt = selsql("select user_name FROM user_info WHERE user_name like " + "'" + user_name + "'");
            string st = Convert.ToString(dt.Rows[0][0]); //st에 저장

            Debug.Log("중복입니다.");
            return false;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("닉네임을 생성하실 수 있습니다.");
            InputUserSQL(user_name);
            return true;
        }
    }

}
