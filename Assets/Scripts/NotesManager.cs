using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//�V���A�����ł��܂���i�I�u�W�F�N�g�̏�Ԃ�ۑ��E�]���ł���j

public class Data
    //�f�[�^�����������߂܂�(json�t�@�C����ǂݍ��ނ��߂̎󂯎M)
    {
        public string name;
        public int maxBlock;
        public int BPM;
        public int offset;
        public Note[] notes;
    }

    [Serializable]
    public class Note
    //�m�[�c�����������߂܂�(json�t�@�C����ǂݍ��ނ��߂̎󂯎M)
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

    //���� = new ����(); =>�N���X�̃C���X�^���X��
    //List<> => �v�f�̒ǉ��E�폜�����邽�߃��X�g���g�p
    public List<int> LaneNum = new List<int>();//�����Ă���m�[�c�̃��[���ԍ�
    public List<int> NoteType = new List<int>();//�m�[�c�̎��
    public List<float> NotesTime = new List<float>();//�m�[�c��������Əd�Ȃ鎞��
    public List<GameObject> NotesObj = new List<GameObject>();//GameObject(�m�[�c�̃I�u�W�F�N�g����)

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;

    void OnEnable()
    //�Y���̃I�u�W�F�N�g���L�������ꂽ�Ƃ��ɌĂяo��
    {
        noteNum = 0;
        songName = "MOB inst 160bpm";
        Load(songName);
    }

    private void Load(string SongName)
    {
        //json�t�@�C����ǂݍ���
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        //Resources.Load(����) => Resources�t�H���_�́u�����v���擾�����
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        //json�t�@�C�����I�u�W�F�N�g�ɕϊ������

        noteNum = inputJson.notes.Length;//���m�[�c����ݒ�

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            //kankaku�́ABPM�~LPB(1���𕪊�������)��60�b�Ŋ�����������
            //BPM��1���Ԃ̔������w������A�����1�b�ԂɃm�[�c�����������Ԋu���킩��
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            //time = (�m�[�c�����������^�C�~���O + json�t�@�C����offset?) �~ 0.01
            NotesTime.Add(time);
            //NotesTime�̃��X�g�ɂ��������߂�time����͂����
            LaneNum.Add(inputJson.notes[i].block);
            //LaneNum�̃��X�g��json�t�@�C����block�̐�������͂����
            NoteType.Add(inputJson.notes[i].type);
            //NoteType�̃��X�g��json�t�@�C����type�̐�������͂����

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z),Quaternion.identity));
            //NotesObj�̃��X�g�ɂ����ǉ�����恨���̂悤�ɐ���(����������́A���W�A��])
        }
    }
}
