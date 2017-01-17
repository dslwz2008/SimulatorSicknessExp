using UnityEngine;
using System.Collections;

public enum MotionState
{
    SpeedUp, Constant, SpeedDown
}

public class ConstantAcceleration : MonoBehaviour {
    public float a = 1.0f;
    public float v1 = 0.0f;
    public float v2 = 0.0f;
    public float vmax = 5.0f;
    public float s1 = 0.0f;
    public float s2 = 0.0f;
    public float timeConstant = 2.0f;
    public float timeTotal = 0.0f;
    public float timeInterval = 2.0f;
    public bool pause = false;
    public MotionState state = MotionState.SpeedUp;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pause)
        {
            return;
        }
        if (state == MotionState.SpeedUp)//匀加速直线运动
        {
            v2 = v1 + a * Time.deltaTime;
            s2 = s1 + v1 * Time.deltaTime + (a * Time.deltaTime * Time.deltaTime) / 2.0f;
            transform.position = new Vector3(0.0f, 0.0f, s2);
            s1 = s2;
            v1 = v2;
            if (v1 > vmax)
            {
                state = MotionState.Constant;
            }
        }
        else if (state == MotionState.Constant)//匀速直线运动
        {
            s2 = s1 + vmax * Time.deltaTime;
            transform.position = new Vector3(0.0f, 0.0f, s2);
            s1 = s2;
            timer += Time.deltaTime;
            if(timer > timeConstant)
            {
                state = MotionState.SpeedDown;
            }
        }
        else if (state == MotionState.SpeedDown)//匀减速直线运动
        {
            a = a > 0 ? -a : a;
            v2 = v1 + a * Time.deltaTime;
            s2 = s1 + v1 * Time.deltaTime + (a * Time.deltaTime * Time.deltaTime) / 2.0f;
            transform.position = new Vector3(0.0f, 0.0f, s2);
            s1 = s2;
            v1 = v2;

            if (v1 < 0.0f)//停止
            {
                pause = true;
                StartCoroutine(OnceMore());
                return;
            }
        }
    }

    IEnumerator OnceMore()
    {
        yield return new WaitForSeconds(timeInterval);
        timer = 0.0f;
        v1 = 0.0f;
        v2 = 0.0f;
        a = a < 0 ? -a : a;
        state = MotionState.SpeedUp;
        pause = false;
    }
}
