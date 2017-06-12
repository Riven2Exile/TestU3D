﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class ui : MonoBehaviour {

    // 配置 
    

	// Use this for initialization
    Camera _cam = null;  //摄像机

    ResLoader _loader = null;
    
    //// 游戏状态变量 start
    bool _is_login = false;

    GameObject _main_person = null;
    //// 游戏状态变量 end


	void Start () {
        Debug.Log("start!!");
        if (AppConst._bUseAB) {
            _loader = ResLoader.CreateLoader(ResLoader.LoadType.ab);
        }
        else {
            _loader = ResLoader.CreateLoader(ResLoader.LoadType.editor);
        }

        //获得摄像机
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera != null)
        {
            Debug.Log("摄像机创建ok");
            _cam = camera.GetComponent<Camera>();
        }

        // 创建UI
        GameObject obj = _loader.Load("Assets/Asset/UI/Login.prefab");
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
                        _main_person = GameObject.Instantiate(person) as GameObject;
                        //
                        _main_person.transform.position = new Vector3(134, 25, 50);
                        _cam.transform.position = new Vector3(94, 60, 20);
                        _cam.transform.LookAt(_main_person.transform.position);

                        // 读取动作
                        GameObject s_run = MyGetResByEditor("Assets/Asset/Char/Model/mage_female_1@run.FBX");


                        _is_login = true; //设置为登录
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
	    
        // todo: 如果是游戏状态下, 开始控制人物移动~
        if (_is_login)
        {
            float w = (Input.GetAxis("Horizontal"));
            if (w != 0.0f) {
                //_main_person.transform.Translate(w * 0.1f, 0, 0);  //// 开始处理人物移动加摄像机跟踪

                //人物的转向
                //_main_person.transform.Rotate(new Vector3(0, 10.0f, 0));
                Vector3 tr = _main_person.transform.position;

                Vector3 tr_temp = new Vector3(tr.x + 10, tr.y, tr.z);
                Vector3 temp = new Vector3(130, 25, 51);
                //_main_person.transform.Rotate(new Vector3(0, 1, 0), 1f, Space.World); //自转

                //_main_person.transform.RotateAround(temp, Vector3.up, 1f); //公转

                Animator ator = _main_person.GetComponent<Animator>();
                ator.Play("run"); // 目前重复跑的方法之一就是直接改变 animator clip 里面的loop 
            }
            else {
                Animator ator = _main_person.GetComponent<Animator>();
                ator.Play("idle");
            }
            
            // 切换状态
            //Animator ator = _main_person.GetComponent<Animator>();
            //ator.Play("run");
        }
	}


    public GameObject MyGetResByEditor(string strPath)
    {
        GameObject obj = _loader.Load(strPath);
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
