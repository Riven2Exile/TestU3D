using UnityEngine;
using System.Collections;

public class MoveObj : MonoBehaviour {

	// Use this for initialization
    public GameObject _obj = null;

    private Vector3 _dest = Vector3.zero;
    public float _speed = 2.0f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(null == _obj){
            return ;
        }


        // Time.deltaTime
        if (_obj.transform.position.Equals(_dest))
        {
            return;
        }


        _obj.transform.LookAt(_dest, Vector3.up);
	}

    void Stop()
    {
        _dest = _obj.transform.position;
    }

    void SetDestPos(Vector3 s)
    {
        _dest = s;
    }
    // 
}
