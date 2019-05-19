using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    Animator animator;

    //Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    // 地面の位置
    private float groundLevel = -3.0f;

    // ジャンプの速度の減衰
    private float dump = 0.8f;

    // ジャンプの速度
    float jumpVelocity = 20;

    // ゲームオーバになる位置
    private float deadLine = -9;

   // public bool isStrongState = false;
   // private float time;

    // Use this for initialization
    void Start()
    {
        // アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        // Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {

        /*レイヤーをかえる作戦が無理だったので、無敵処理は　CubeControllerに書く

        //無敵処理（レイヤーを変える）→レイヤーを変えても衝突はする！
        if (isStrongState) { //this.gameObject.layer = 2;
            time = 0;
            Debug.Log(gameObject.layer);
            Debug.Log(isStrongState);
            isStrongState = false;  //ﾄﾘｶﾞに使うので一瞬だけここのif文を実行するのですぐにfalseに戻す
        }

        //セットしたタイマーを減らす
        time += Time.deltaTime;
        //ボツ、レイヤーを変えても衝突はするため
        if (time < 10f) { this.gameObject.layer = 2;
        }
        else
        {
            this.gameObject.layer = 1;
        }

        */




        // 走るアニメーションを再生するために、Animatorのパラメータを調節する
        this.animator.SetFloat("Horizontal", 1);

        // 着地しているかどうかを調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);

        // ジャンプ状態のときにはボリュームを0にする（追加）
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        // 着地状態でクリックされた場合
        if (Input.GetMouseButtonDown(0) && isGround)
        {
            // 上方向の力をかける
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        // クリックをやめたら上方向への速度を減速する
        if (Input.GetMouseButton(0) == false)
        {
            if (this.rigid2D.velocity.y > 0)
            {
                this.rigid2D.velocity *= this.dump;
            }
        }

        // デッドラインを超えた場合ゲームオーバにする
        if (transform.position.x < this.deadLine)
        {
            // UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            // ユニティちゃんを破棄する
            Destroy(gameObject);
        }

    }
}