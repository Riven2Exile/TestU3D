using UnityEngine;
using System.Collections;
using UnityEditor;

public class ui : MonoBehaviour {

	// Use this for initialization
    Camera _cam = null;

	void Start () {
        Debug.Log("start!!");

        //获得摄像机
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera != null)
        {
            Debug.Log("摄像机创建ok");
            _cam = camera.GetComponent<Camera>();
        }

        // 创建UI
        GameObject obj = AssetDatabase.LoadAssetAtPath("Assets/Asset/UI/Login.prefab", typeof(UnityEngine.Object)) as GameObject;
        if (obj)
        {
            GameObject go = GameObject.Instantiate(obj) as GameObject;
            go.SetActive(true);

            // 先写死取子按钮, 设置绑定
            UnityEngine.Transform ctrl_obj = go.transform.FindChild("login");
            if (ctrl_obj != null)
            {
                ctrl_obj = ctrl_obj.FindChild("loginScene");
                if(ctrl_obj != null)
                {
                    ctrl_obj = ctrl_obj.FindChild("btn-start");
                    if (ctrl_obj == null) { Debug.Log("btn-start not found"); }
                }
                else{
                    Debug.Log("loginScene 没找到");
                }
            }
            else
            {
                Debug.Log("login 没找到");
            }
            UIWrapper ui = ctrl_obj.GetComponent<UIWrapper>();
            if(ui != null){

                ////// todd : 按钮响应 - >
                ui.BindButtonClick((o) => {
                    Debug.Log("加载一个场景");

                    GameObject scene = MyGetResByEditor("Assets/Asset/Scene/MainGround_wfpy.prefab");
                    GameObject scene_prefab = GameObject.Instantiate(scene) as GameObject;
                    if (scene_prefab == null)
                    {
                        Debug.Log("场景prefab实例化失败");
                    }
                    else
                    {
                        Debug.Log("scene_prefab.SetActive");
                        scene_prefab.SetActive(true);

                        go.SetActive(false); //UI隐藏掉

                        // 放一个人物:
                        GameObject person = MyGetResByEditor("Assets/Asset/Char/Model/mage_female_1_Prefab.prefab");
                        GameObject person_prefab = GameObject.Instantiate(person) as GameObject;
                        //
                        person_prefab.transform.position = new Vector3(134,25,50);
                        _cam.transform.position = new Vector3(94, 60, 20);
                        _cam.transform.LookAt(person_prefab.transform.position);
                    }
                        
                    
                }); //加载一个场景
            }
        }
        else
        {
            Debug.Log("UI创建失败");
        }

        GameObject.DontDestroyOnLoad(CreateEventSystem()); //创建事件系统
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public GameObject MyGetResByEditor(string strPath)
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath(strPath, typeof(UnityEngine.Object)) as GameObject;
        if (obj == null)
        {
            Debug.Log("创建" + strPath + "失败");
            return null;
        }
        else
        {
            return obj;
        }
    }

    public static GameObject CreateEventSystem()
    {
        if (UnityEngine.EventSystems.EventSystem.current != null)
            return UnityEngine.EventSystems.EventSystem.current.gameObject;
        GameObject eventsystem = new GameObject("EventSystem");
        eventsystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
#if UNITY_EDITOR
        eventsystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
#endif
        eventsystem.AddComponent<UnityEngine.EventSystems.TouchInputModule>();

#if UNITY_EDITOR
        Application.runInBackground = true;
        //Hierarchy.EventSystem.GetComponent<UnityEngine.EventSystems.StandaloneInputModule>().forceModuleActive = true;
#endif

        return eventsystem;
    }
}
