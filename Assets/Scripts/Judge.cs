using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField] private GameObject[] MessageObj;//プレイヤーに判定を伝えるオブジェクト
    [SerializeField] NotesManager notesManager;//スクリプト「notesManager」を入れる変数
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))//Dキーが押されたとき
        {
            if (notesManager.LaneNum[0] == 0)
            {
                //本来ノーツをたたく場所と実際にたたいた場所のずれの値の絶対値をJudgement関数に送る
                Judgement(GetABS(Time.time - notesManager.NotesTime[0]));
                //ABS => 絶対値
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (notesManager.LaneNum[0] == 1)
            {
                Judgement(GetABS(Time.time - notesManager.NotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (notesManager.LaneNum[0] == 2)
            {
                Judgement(GetABS(Time.time - notesManager.NotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (notesManager.LaneNum[0] == 3)
            {
                Judgement(GetABS(Time.time - notesManager.NotesTime[0]));
            }
        }

        if (Time.time > notesManager.NotesTime[0] + 0.2f)//ノーツの位置から0.2秒経っても入力がなかった場合
        {
            Message(3);
            DeleteData();
            Debug.Log("Miss");
        }

    }
    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10)//ずれが0.1秒以下なら
        {
            Debug.Log("Perfect");
            Message(0);
            DeleteData();
        }
        else
        {
            if (timeLag <= 0.15)
            {
                Debug.Log("Great");
                Message(1);
                DeleteData();

            }
            else
            {
                if (timeLag <= 0.2)
                {
                    Debug.Log("Bad");
                    Message(2);
                    DeleteData();

                }

            }
        }
    }
    float GetABS(float num)
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }
    void DeleteData()//すでにたたいたノーツを削除
    {
        //ノーツの判定線と重なる時間・レーン番号・ノーツの種類の情報を削除
        notesManager.NotesTime.RemoveAt(0);
        notesManager.LaneNum.RemoveAt(0);
        notesManager.NoteType.RemoveAt(0);
        //RemoveAt => リストの要素を削除する
    }
    void Message(int judge)//判定を表示
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.LaneNum[0] - 1.5f,0.76f,0.15f),Quaternion.Euler(45, 0, 0));
        //Instantiate => 生成（物(判定メッセージ),場所(該当レーンの判定線の上).回転(X軸方向に45°回転)
    }
}
