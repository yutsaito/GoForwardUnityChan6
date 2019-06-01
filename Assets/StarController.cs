using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    // 星の移動速度
    private float speed = -0.2f;
    // 消滅位置
    private float deadLine = -10;

    //GameObject myplayer;

    // Start is called before the first frame update
    void Start()
    {
        //myplayer = GameObject.Find("UnityChan2D"); 
    }

    // Update is called once per frame
    void Update()
    {
        // 星を移動させる
        transform.Translate(this.speed, 0, 0);

        // 画面外に出たら破棄する
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    //浮ケ谷さん
    //previousTimeはそれぞれのcubeが持っています。
   // 無敵状態で衝突したcubeであれば ｀previousTime = time｀ が実行されるので
        //previousTimeにtimeが代入されますが、それ以外のcubeは代入が実行されないので0という状態になっています。
        //というアドバイスをいただいた。 UnitychanCOntrollerにもっていく
        //ここの衝突判定はここでいいだろう　　→　判定処理の時の　timeを０に戻す関係で、UnityChanControllerに移す
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Tag")
        {

            Debug.Log("Got a star!");
            Destroy(this.gameObject);

            //無敵にする
            //レイヤーをかえる作戦が無理だったので、無敵処理は　CubeControllerに書く
            //myplayer.GetComponent<UnityChanController>().isStrongState = true;
            //全てのｲﾝｽﾀﾝｽを取得するのが面倒なので static の変数にする。UnitychanとあたったらDestroyする
            UnityChanController.isStrongState = true;
            //Debug.Log(UnityChanController.isStrongState);
        }
    }
*/


}
