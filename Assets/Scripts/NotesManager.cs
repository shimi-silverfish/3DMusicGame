using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//シリアル化できますよ（オブジェクトの状態を保存・転送できる）

public class Data
    //データが持つ情報を決めます(jsonファイルを読み込むための受け皿)
    {
        public string name;
        public int maxBlock;
        public int BPM;
        public int offset;
        public Note[] notes;
    }

    [Serializable]
    public class Note
    //ノーツが持つ情報を決めます(jsonファイルを読み込むための受け皿)
    {
        public int type;
        public int num;
        public int block;
        public int LPB;
    }
public class NotesManager : MonoBehaviour
{
    public int noteNum;
    private string songName;

    //○○ = new ○○(); =>クラスのインスタンス化
    //List<> => 要素の追加・削除があるためリストを使用
    public List<int> LaneNum = new List<int>();//落ちてくるノーツのレーン番号
    public List<int> NoteType = new List<int>();//ノーツの種類
    public List<float> NotesTime = new List<float>();//ノーツが判定線と重なる時間
    public List<GameObject> NotesObj = new List<GameObject>();//GameObject(ノーツのオブジェクト自体)

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;

    void OnEnable()
    //該当のオブジェクトが有効化されたときに呼び出し
    {
        noteNum = 0;
        songName = "MOB inst 160bpm";
        Load(songName);
    }

    private void Load(string SongName)
    {
        //jsonファイルを読み込む
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        //Resources.Load(○○) => Resourcesフォルダの「○○」を取得するよ
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        //jsonファイルをオブジェクトに変換するよ

        noteNum = inputJson.notes.Length;//総ノーツ数を設定

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            //kankakuは、BPM×LPB(1拍を分割した数)を60秒で割った数だよ
            //BPMは1分間の拍数を指すから、これで1秒間にノーツが生成される間隔がわかる
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            //time = (ノーツが生成されるタイミング + jsonファイルのoffset?) × 0.01
            NotesTime.Add(time);
            //NotesTimeのリストにさっき決めたtimeを入力するよ
            LaneNum.Add(inputJson.notes[i].block);
            //LaneNumのリストにjsonファイルのblockの数字を入力するよ
            NoteType.Add(inputJson.notes[i].type);
            //NoteTypeのリストにjsonファイルのtypeの数字を入力するよ

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z),Quaternion.identity));
            //NotesObjのリストにこれを追加するよ→このように生成(生成するもの、座標、回転)
        }
    }
}
