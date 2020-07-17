using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocityAttack : MonoBehaviour
{
    [SerializeField] Transform endPos;  //終点座標
    [SerializeField] float flightTime = 2;  //到達時間。
    [SerializeField] float speedRate = 1;   //滞空時間を基準とした移動速度倍率
    private const float gravity = -9.8f;    //重力
    [SerializeField] float heightRate = 1;  //投てきの高さの倍率。
    /// <summary>
    /// 射出するオブジェクト
    /// </summary>
    [SerializeField, Tooltip("射出するオブジェクトをここに割り当てる")]
    private GameObject ThrowingObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //todo 仮。
        //左クリックした時、玉を上から発射する。
        if (Input.GetMouseButtonDown(0))        
        {
            StartCoroutine(Jump(endPos.position, flightTime, speedRate, gravity * heightRate));
        }
        //右クリックした時、玉を正面から発射する。
        if(Input.GetMouseButtonDown(1))
        {
           StartCoroutine(ForwardAttack(endPos.position, flightTime, speedRate));
        }

        //マウスホイールを押し込んだ時。
        if(Input.GetMouseButtonDown(2))
        {
         //   StartCoroutine(RightAttackes(endPos.position, flightTime, speedRate, gravity * heightRate));
            StartCoroutine(LeftAttack(endPos.position, flightTime, speedRate, gravity * heightRate));
        }

    }

    // 現在位置からendPosへの放物運動　
    private IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        //// Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);

        var startPos = transform.position; // 初期位置
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            ball.transform.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        ball.transform.position = endPos;
    }

    //左攻撃。
    private IEnumerator LeftAttacks(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        //// Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);

        var startPos = transform.position; // 初期位置
        var diffX = (endPos - startPos).x; // 始点と終点のy成分の差分
        var diffZ = (endPos - startPos).z; // 始点と終点のy成分の差分
        var vnx = (diffX - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn
        var vnz = (diffZ - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        //X成分の差のほうが大きいとき。
        if (diffX > diffZ)
        {
            // 放物運動
            for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
            {
                var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
                p.x = startPos.x + vnx * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
                ball.transform.position = p;
                yield return null; //1フレーム経過
            }
        }
        //Z成分の差のほうが大きいとき。

        if (diffX < diffZ)
        {
            // 放物運動
            for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
            {
                var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
                p.z = startPos.z - vnz * t - 0.5f * gravity * t * t; // 鉛直方向の座標 y
                ball.transform.position = p;
                yield return null; //1フレーム経過
            }
        }
        
        // 終点座標へ補正
        ball.transform.position = endPos;
    }

    //右攻撃。
    private IEnumerator RightAttackes(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        //// Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);

        var startPos = transform.position; // 初期位置
        var diffX = (endPos - startPos).x; // 始点と終点のy成分の差分
        var diffZ = (endPos - startPos).z; // 始点と終点のy成分の差分
        var vnx = (diffX - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn
        var vnz = (diffZ - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        //X成分の差のほうが大きいとき。
        if (diffX > diffZ)
        {
            // 放物運動
            for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
            {
                var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
                p.x = startPos.x - vnx * t - 0.5f * gravity * t * t; // 鉛直方向の座標 y
                ball.transform.position = p;
                yield return null; //1フレーム経過
            }
        }
        //Z成分の差のほうが大きいとき。
        if (diffX < diffZ)
        {
            // 放物運動
            for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
            {
                var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
                p.z = startPos.z - vnz * t - 0.5f * gravity * t * t; // 鉛直方向の座標 y
                ball.transform.position = p;
                yield return null; //1フレーム経過
            }
        }

        // 終点座標へ補正
        ball.transform.position = endPos;
    }

    //左攻撃(諦めたやつ)。
    private IEnumerator LeftAttack(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        //// Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);

        var startPos = transform.position; // 初期位置
        var diff = endPos - startPos; // 始点と終点のy成分の差分; // 始点と終点のx成分の差分

        Vector3 y = new Vector3(0, 1, 0);     //敵の直上ベクトル。
        var crossVec = Vector3.Cross(diff, y);        //外積を使ってプレイヤー方向の垂線を求める。
        var crossVecNormalize = crossVec.normalized;                //正規化。
        //todo 逆にするときはがいせきのあたいをぎゃくにしてあげるうｐ。
        var vn = (/*diffY - */gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            p += crossVecNormalize * (vn * t - 0.5f * gravity * t * t);// 鉛直方向の座標 x
            ball.transform.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        ball.transform.position = endPos;
    }


    /// <summary>
    /// 正面の攻撃。
    /// </summary>
    /// <param name="endPos">終点</param>
    /// <param name="flightTime">敵に玉が衝突するまでの時間</param>
    /// <param name="speedRate">スピード倍率</param>
    private IEnumerator ForwardAttack(Vector3 endPos, float flightTime, float speedRate)
    {
        // Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, transform.position, Quaternion.identity);

        var startPos = this.transform.position;                         //初期座標。
        var distance = endPos - startPos;                               //玉までの距離。    
        var processingNum = Time.deltaTime * speedRate / flightTime;    //処理の回数。
        var movementOneFrame = distance * processingNum;                //1Frameあたりの移動量。


        for (var i = 0f; i < 1; i += (Time.deltaTime * speedRate / flightTime))
        {
            ball.transform.position += movementOneFrame;
            yield return null; //1フレーム経過
        }
        ball.transform.position += endPos;

    }
}
