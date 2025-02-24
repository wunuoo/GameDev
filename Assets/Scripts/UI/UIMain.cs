using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoSingleton<UIMain>
{
    public Button button_NextChapter;

    public Button button_SuanPan;
    public Button button_Back;
    public GameObject button_Cheat;


    bool suanPanMode;

    public UIGuide uiGuide;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            button_Cheat.gameObject.SetActive(!button_Cheat.gameObject.activeSelf);
        }
    }

    public void OnClickGuide()
    {
        uiGuide.showed = !uiGuide.showed;
        uiGuide.gameObject.SetActive(uiGuide.showed);
    }



    protected override void OnStart()
    {
        EventManager.Instance.OnChapterFinish += this.Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnChapterFinish -= this.Refresh;
    }

    void Refresh()
    {
        button_NextChapter.gameObject.SetActive(ChapterManager.Instance.canGoNextChapter);
        this.button_SuanPan.gameObject.SetActive(!suanPanMode);
        this.button_Back.gameObject.SetActive(suanPanMode);
    }

    public void OnClickNextChapter()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        ChapterManager.Instance.StartNewChapter();
        Refresh();
    }

    public void OnClickSuanPan()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        SceneManager.Instance.LoadScene("SuanPan");
        suanPanMode = true;
        Refresh();
    }

    //返回主界面
    public void OnClickBack()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        SceneManager.Instance.LoadScene("Main");
        suanPanMode = false;
        Refresh();
    }

    public void OnClickTools()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        UIManager.Instance.Show<UITools>();
    }

    public void OnClickCharInfo()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        UIManager.Instance.Show<UICharInfo>();
    }

    public void OnClickPause()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        UIManager.Instance.Show<UIPauseGame>();
    }

    public void OnClickRecord()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        UIManager.Instance.Show<UIRecord>();
    }

    //测试用按钮，方便跳过任务
    public void OnClickCheat()
    {
        Task task = TaskManager.Instance.currentTask;
        if (task != null)
        {
            //触发一次检测中间结果，必定成功
            TaskManager.Instance.CheckResult(task.results[TaskManager.Instance.resultIndex]);
        }
        
    }
}
