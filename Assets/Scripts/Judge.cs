using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    //�ϐ��̐錾
    [SerializeField] private GameObject[] MessageObj;//�v���C���[�ɔ����`����I�u�W�F�N�g
    [SerializeField] NotesManager notesManager;//�X�N���v�g�unotesManager�v������ϐ�
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))//D�L�[�������ꂽ�Ƃ�
        {
            if (notesManager.LaneNum[0] == 0)
            {
                //�{���m�[�c���������ꏊ�Ǝ��ۂɂ��������ꏊ�̂���̒l�̐�Βl��Judgement�֐��ɑ���
                Judgement(GetABS(Time.time - notesManager.NotesTime[0]));
                //ABS => ��Βl
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

        if (Time.time > notesManager.NotesTime[0] + 0.2f)//�m�[�c�̈ʒu����0.2�b�o���Ă����͂��Ȃ������ꍇ
        {
            Message(3);
            DeleteData();
            Debug.Log("Miss");
        }

    }
    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10)//���ꂪ0.1�b�ȉ��Ȃ�
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
    void DeleteData()//���łɂ��������m�[�c���폜
    {
        //�m�[�c�̔�����Əd�Ȃ鎞�ԁE���[���ԍ��E�m�[�c�̎�ނ̏����폜
        notesManager.NotesTime.RemoveAt(0);
        notesManager.LaneNum.RemoveAt(0);
        notesManager.NoteType.RemoveAt(0);
        //RemoveAt => ���X�g�̗v�f���폜����
    }
    void Message(int judge)//�����\��
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.LaneNum[0] - 1.5f,0.76f,0.15f),Quaternion.Euler(45, 0, 0));
        //Instantiate => �����i��(���胁�b�Z�[�W),�ꏊ(�Y�����[���̔�����̏�).��](X��������45����])
    }
}
