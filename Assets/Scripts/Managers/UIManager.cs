using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public virtual void OnClose()
    {
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        //UIManager.Instance.DeleteInstance(this.gameObject);
    }
}

public class UIManager : Singleton<UIManager>
{

    public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    //public Dictionary<Type, GameObject> uiInstances = new Dictionary<Type, GameObject>();

    //public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    // Start is called before the first frame update
    public UIManager()
    {
        uiNames.Add(typeof(UIDialog), "UIDialog");
        uiNames.Add(typeof(UISetting), "UISetting");
        uiNames.Add(typeof(UITools), "UITools");
        uiNames.Add(typeof(UIPauseGame), "UIPauseGame");
    }

    public T Show<T>()
    {
        GameObject go;
        //if (uiInstances.TryGetValue(typeof(T), out go)) //说明UI实例存在
        //{
        //    Debug.Log("find prefabHidden");
        //    go.SetActive(true); // 显示已经实例化但被隐藏的预制体
        //}
        //else
        //{
            string name = "UI/" + uiNames[typeof(T)];
            go = GameObject.Instantiate(Resources.Load<GameObject>(name)); // 在当前位置实例化预制体
            //if (go != null)
            //{
            //    uiInstances.Add(typeof(T), go);
            //}
            //else Debug.LogError("找不到文件：" + name);
        //}
            if(go == null)
            {
                Debug.LogError("找不到文件：" + name);
                return default(T);
            }
        return go.GetComponent<T>();
    }

    //public void Hide(Type type)
    //{
    //    GameObject go;
    //    if(uiInstances.TryGetValue(type, out go)) //说明UI实例存在
    //    {
    //        Debug.Log("find prefabToHide");
    //        go.SetActive(false); // 隐藏实例化的预制体
    //    }
    //    else
    //    {
    //        Debug.Log("can't find prefabToHide");
    //    }
 
    //}

    //internal void DeleteInstance(GameObject obj)//当ui关闭的时候应该注销
    //{
    //    GameObject ui;
    //    if(uiInstances.TryGetValue(obj.GetType(), out ui))
    //    {
    //        uiInstances.Remove(obj.GetType());
    //    }

    //}
}
