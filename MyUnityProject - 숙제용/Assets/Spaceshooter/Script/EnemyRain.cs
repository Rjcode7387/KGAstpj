using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRain : MonoBehaviour
{
    const float CREAT_INTERVAL = 0.18f;
    float mCreatTime = 0;
    float mTotalTime = 0;

    float mNextCreateInterval = CREAT_INTERVAL;

    int mPhase = 1;
    public GameObject mEnemy;

    private void Update()
    {
        mTotalTime += Time.deltaTime;
        mCreatTime += Time.deltaTime;
        if (mCreatTime > mNextCreateInterval)
        {
            mCreatTime = 0;
            mNextCreateInterval = CREAT_INTERVAL - (0.005F*mTotalTime);
            if (mNextCreateInterval  < 0.005f)
            {
                mNextCreateInterval = 0.005f;
            }
            for (int i = 0; i < mPhase * 0.2f; i++)
            {
                craetEnemy(14f+i*0.2f);
            }
        }
        if (mTotalTime >= 10f)
        {
            mTotalTime = 0;
            mPhase++;
        }
    }
    private void craetEnemy(float y)
    {
        float x = Random.Range(-6f, 6f);
        createObject(mEnemy, new Vector3(x, y, 0), Quaternion.identity);

    }
    private GameObject createObject(GameObject original, Vector3 position, Quaternion rotation)
    {
        return (GameObject)Instantiate(original, position, rotation);
    }
    
}
