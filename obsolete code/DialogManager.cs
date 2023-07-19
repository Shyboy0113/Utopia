using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class DialogueManager : MonoBehaviour
{
    public string csvFilePath; // CSV 파일 경로
    public Text speakerText; // 말하는 사람을 표시할 UI 텍스트
    public Text dialogueText; // 대화 내용을 표시할 UI 텍스트

    private List<string[]> dialogueData; // 대화 데이터를 저장한 리스트
    private int currentLine; // 현재 대화의 인덱스

    void Start()
    {
        dialogueData = new List<string[]>();
        LoadDialogueData();
        currentLine = 0;
        DisplayNextLine();
    }

    void LoadDialogueData()
    {
        //StreamReader = 텍스트 파일 불러오는 기능
        StreamReader reader = new StreamReader(csvFilePath);

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            dialogueData.Add(values);
        }

        reader.Close();
    }
    
    void DisplayNextLine()
    {
        if (currentLine < dialogueData.Count)
        {
            string[] lineData = dialogueData[currentLine];

            string speaker = lineData[0]; // 말하는 사람
            string dialogue = lineData[1]; // 대화 내용

            speakerText.text = speaker;
            dialogueText.text = dialogue;

            currentLine++;
        }
        else
        {
            // 대화가 끝난 경우 처리할 내용 추가
        }
    }
    
}