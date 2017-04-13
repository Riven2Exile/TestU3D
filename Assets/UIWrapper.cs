using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIWrapper : MonoBehaviour, IPointerClickHandler
{

    private event Action<GameObject> onClick;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 重载点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        // todo: 回调
        if (onClick != null)
        {
            onClick(this.gameObject);
        }
    }

    //只绑定一个点击事件
    public void BindButtonClick(Action<GameObject> onClick)
    {
        if (onClick == null)
            return;
        this.onClick = onClick;
    }

}
