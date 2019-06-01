using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    // 星のPrefab
    public GameObject starPrefab;

    // 時間計測用の変数
    private float delta = 0;

    // 星の生成間隔
    private float span = 10.0f;

    // 星の生成位置：X座標
    private float genPosX = 10f;
    // 星の生成位置：Y座標
    private float genPosY = 3f;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

        // span秒以上の時間が経過したかを調べる
        if (this.delta > this.span)
        {
            this.delta = 0;
                // 星の生成
                GameObject go = Instantiate(starPrefab) as GameObject;
                go.transform.position = new Vector2(this.genPosX, this.genPosY);
        }
    }
}