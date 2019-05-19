using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    // 星の移動速度
    private float speed = -0.2f;
    // 消滅位置
    private float deadLine = -10;

    GameObject myplayer;

    // Start is called before the first frame update
    void Start()
    {
        myplayer = GameObject.Find("UnityChan2D"); 
    }

    // Update is called once per frame
    void Update()
    {
        // キューブを移動させる
        transform.Translate(this.speed, 0, 0);

        // 画面外に出たら破棄する
        if (transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }
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
            CubeController.isStrongState = true;
            
        }
    }



}
