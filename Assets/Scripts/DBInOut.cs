using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;     //C#의 데이터 테이블 때문에 사용
using MySql.Data;     //MYSQL함수들을 불러오기 위해서 사용
using MySql.Data.MySqlClient;    //클라이언트 기능을사용하기 위해서 사용
using System;

public class DBInOut : MonoBehaviour
{
    //유저 닉네임, 점수 전역변수로 받은 후 db갱신

    public void InputUserSQL() //유저 프로필 생성
    {
        string nickName = "testuser99";
        DBTester db = this.gameObject.AddComponent<DBTester>();
        db.sqlcmdall("insert into user_info(user_name) values('+"+ nickName+"')");
    }

    public void UpdateUserSQL() //유저 점수 갱신
    {
        DBTester db = this.gameObject.AddComponent<DBTester>();
        string nickName = "testuser1";
        int score = 101;
        db.sqlcmdall("update user_info set user_score="+score+" where user_name="+"'"+ nickName + "'");

    }

    public DataTable GetUserScoreSQL() //유저 점수 String 형태로 가져옴
    {
        string nickName = "testuser1";
        DBTester db = this.gameObject.AddComponent<DBTester>();
        DataTable dt = db.selsql("select user_score from user_info where user_name like "+"'"+nickName+"'");
        string st = Convert.ToString(dt.Rows[0][0]); //st에 저장

        Debug.Log("유저 점수 : " + st);
        return dt;
    }

    public DataTable GetRankingSQL() //유저 랭킹
    {
        DBTester db = this.gameObject.AddComponent<DBTester>();
        DataTable dt = db.selsql("SELECT"+
                                    " user_name, user_score," +
                                    " (SELECT COUNT(*) + 1 FROM user_info WHERE user_score > b.user_score) AS rank" +
                                    " FROM"+
                                    " user_info AS b" +
                                    " ORDER BY"+
                                    " rank ASC");

        /*for(int i = 0; i<10; i++)
        {    
                Debug.Log("유저이름 : " + Convert.ToString(dt.Rows[i][0])+" 점수 : " + Convert.ToString(dt.Rows[i][1]));         
        }*/

        return dt;
    }

    public Boolean IdCheckSQL() //유저 닉네임 중복체크
    {
        //Boolean nickCheck = true;
        string nickName = "testuser12";
        try
        {
            DBTester db = this.gameObject.AddComponent<DBTester>();
            DataTable dt = db.selsql("select user_name FROM user_info WHERE user_name like "+"'"+nickName+"'");
            string st = Convert.ToString(dt.Rows[0][0]); //st에 저장

            Debug.Log("중복입니다.");
            return false;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("닉네임을 생성하실 수 있습니다.");
            return true ;
        }
    }

}
