using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

//这个Manager掌控了游戏的剧情进度，相关属性
public class ChapterManager : MonoSingleton<ChapterManager>
{
    public PlayableDirector director;
    public Image ppt;

    public bool canGoNextChapter = false;

    public List<Chapter> chapters = new List<Chapter>();
    Chapter currentChapter;

    //这些指针放在这里而非Chapter中，这是因为ScriptableObj虽然在部署后不会保存值，但在编辑器会，重新调非常麻烦
    int chapterIndex = 0;
    int dialogIndex = 0;
    int taskIndex = 0;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        ppt.gameObject.SetActive(true);
        director.stopped += OnBeginAnimStop;
        director.Play();
        
        
    }

    void OnBeginAnimStop(object arg)
    {
        ppt.gameObject.SetActive(false);
        this.StartChapter(chapterIndex);
        director.stopped -= OnBeginAnimStop;
    }

    //一个章节的基本流程是：开场对话 --》 分配任务 --》 完成任务 --》 成功对话 --》 分配任务 。。。 --》 进入章节后空闲 --》 下一章开场对话
    void StartChapter(int index)
    {
        currentChapter = chapters[index];
        dialogIndex = 0;
        taskIndex = 0;
        canGoNextChapter = false;
        AssignNewDialog();//开场对话
        
    }

    public void StartNewChapter()
    {
        if(canGoNextChapter)
            StartChapter(chapterIndex);
    }

    internal void AssignNewDialog()
    {
        DialogManager.Instance.PlayDialog(currentChapter.dialogs[dialogIndex]);
        dialogIndex++;
    }

    internal void AssignSceneChangeDialog()
    {
        AssignNewDialog();
        SceneManager.Instance.loadCompleted -= AssignSceneChangeDialog;
    }

    internal void OnChapterFinished()
    {
        canGoNextChapter = true;
        chapterIndex++;
    }

    internal void AssignNewTask()
    {
        TaskManager.Instance.StartNewTask(currentChapter.tasks[taskIndex]);
        taskIndex++;
    }


}
