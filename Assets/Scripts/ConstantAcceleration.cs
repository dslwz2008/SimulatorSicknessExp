using UnityEngine;
using System.Collections;

public class ConstantAcceleration : MonoBehaviour {
    public float a = 1.0f;
    public float v1 = 0.0f;
    public float v2 = 0.0f;
    public float vmax = 5.0f;
    public float s1 = 0.0f;
    public float s2 = 0.0f;
    public float timeSetting = 10.0f;
    public float timeTotal = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeTotal += Time.deltaTime;
        if (timeTotal > timeSetting)
        {
            a = a > 0 ? -a:a;
            if (v1 < 0.0f)//停止
            {
                return;
            }
            else//匀减速直线运动
            {
                v2 = v1 + a * Time.deltaTime;
                s2 = s1 + v1 * Time.deltaTime + (a * Time.deltaTime * Time.deltaTime) / 2.0f;
                transform.position = new Vector3(s2, 0.0f, 0.0f);
                s1 = s2;
                v1 = v2;
            }
        }
        else
        {
            if (v1 > vmax)//匀速直线运动
            {
                s2 = s1 + vmax * Time.deltaTime;
                transform.position = new Vector3(s2, 0.0f, 0.0f);
                s1 = s2;
            }
            else//匀加速直线运动
            {
                v2 = v1 + a * Time.deltaTime;
                s2 = s1 + v1 * Time.deltaTime + (a * Time.deltaTime * Time.deltaTime) / 2.0f;
                transform.position = new Vector3(s2, 0.0f, 0.0f);
                s1 = s2;
                v1 = v2;
            }
        }

    }
}
