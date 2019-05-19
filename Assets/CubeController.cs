using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour
{

    // キューブの移動速度
    private float speed = -0.2f;

    // 消滅位置
    private float deadLine = -10;

    // public AudioClip block;

    private bool isAlreadyPlayed = false;

    //GameObject myPlayer = GameObject.Find("UnityChan2D");

    //無敵状態変数
    static public bool isStrongState = false;
    private float time=10f;

    // Use this for initialization
    void Start()
    {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        string yourTag = collision.gameObject.tag;

        if (yourTag == "Player_Tag" && isStrongState==true) {
            time = 0f;
            isStrongState = false;  //ﾀｲﾏｽﾀｰﾄのﾄﾘｶﾞにつかうので時間だけｾｯﾄしてすぐに戻す
        }

        time += Time.deltaTime;
        if (time < 10f) { Destroy(this.gameObject); }





        if (!this.isAlreadyPlayed)
        {
            this.isAlreadyPlayed = true;

            //Debug.Log(this.isAlreadyPlayed);
           // string yourTag = collision.gameObject.tag;


            Debug.Log(yourTag);     //OKだった・・・

            // ボリュームを0.3にする（追加）
            //GetComponent<AudioSource>().volume = 0.3f;


            //なぜか、タッ・タッ・タンといかない（ひとつの再生がおわるまで他が再生できない？）→AudioSource.PlayOneShot(AudioClip clip);でないといけないらしい
            /*というわけで GetComponent<AudioSource>().volume = 0.3f;　はボツ！*/
            if (yourTag == "Cube_Tag")
            {
                // ボリュームを0.3にする（追加）
                //GetComponent<AudioSource>().volume = 0.3f;
                //this.GetComponent<AudioSource>().PlayOneShot(block); //調べたのにこれもダメだった・・・

                AudioClip clip = gameObject.GetComponent<AudioSource>().clip;
                gameObject.GetComponent<AudioSource>().PlayOneShot(clip);       //このままでは多くなりすぎてしまう！一回なったらなくしたい！→isAlreadyPlayedを追加→できた！！
                Debug.Log("Played by the other Cube");
            }
            else if (yourTag == "Ground_Tag")
            {
                //GetComponent<AudioSource>().volume = 0.3f;
                // this.GetComponent<AudioSource>().PlayOneShot(block);
                AudioClip clip = gameObject.GetComponent<AudioSource>().clip;
                gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
                Debug.Log("Played by the Ground");

            }
            //

        }




    }
}