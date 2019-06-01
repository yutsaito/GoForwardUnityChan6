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

    //無敵状態変数　　はUnityChanControllerにもっていく
    static public bool isStrongState = false;
    //private float time;
    public float time;          
  
    // 無敵時移動速度
    private float speed = 0.0f;
    private float strongTime = 10.0f;

    // Use this for initialization
    void Start()
    {
        // アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        // Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();

        time = 10f;  //      無敵処理はUnitychanContorollerにもっていく
  

    }

    // Update is called once per frame
    void Update()
    {

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

        //無敵の10秒間　　はUnitychanControllerに持っていく
        time += Time.deltaTime;
        // 無敵の間少し前に移動させる
        if (time  < strongTime && this.transform.position.x < -2.9f) { this.rigid2D.AddForce(new Vector2(1.0f, 0)); ; }
    }

    //浮ケ谷さんのｱﾄﾞﾊﾞｲｽにより、無敵化をこっちに移す
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star_Tag")
        {

            Debug.Log("Got a star!");
            Destroy(collision.gameObject);

            //無敵にする
            UnityChanController.isStrongState = true;
            time = 0f;
            isStrongState = false;  //ﾀｲﾏｽﾀｰﾄのﾄﾘｶﾞにつかうので時間だけｾｯﾄしてすぐに戻す
            strongTime *= 0.7f;
        }
    }




    //浮ケ谷さんのｱﾄﾞﾊﾞｲｽにより、無敵化をこっちに移す
    private void OnCollisionEnter2D(Collision2D collision)  //OnTrigger→OnCollision  Corider2D→Collision2D
    {
        string yourTag = collision.gameObject.tag;

        //無敵処理は UnityChanControllerにもっていく
        if (time < strongTime　&& (yourTag=="Cube_Tag")) { Destroy(collision.gameObject); }
    }



}