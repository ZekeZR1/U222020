using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
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

    //攻撃タイプ。
    public enum AttackType{
        enFrontAttack,            //正面攻撃。
        enUpAttack,               //上攻撃。
        enRightAttack,            //右攻撃。
        enLeftAttack,             //左攻撃
        enAttackTypeNum           //攻撃の種類の数。
    };

    public AttackType attackType = AttackType.enAttackTypeNum;      //攻撃タイプ。

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //todo TEST.
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Attack(AttackType.enLeftAttack);
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Attack(AttackType.enUpAttack);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Attack(AttackType.enFrontAttack);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Attack(AttackType.enRightAttack);
        //}
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Attack(AttackType.enLeftAttack);
        //    Attack(AttackType.enUpAttack);
        //    Attack(AttackType.enFrontAttack);
        //    Attack(AttackType.enRightAttack);
        //}
    }

    /// <summary>
    /// 攻撃。
    /// </summary>
    /// <param name="attackNum">攻撃の番号</param>
    public void Attack(AttackType attackNum)
    {
        //攻撃パターンが存在していたなら。
        if (attackNum < AttackType.enAttackTypeNum)
        {
            //todo　ここに共通の処理を入れてもいいかも。

            switch (attackNum)
            {
                case AttackType.enFrontAttack:    //前方攻撃のとき。

                    StartCoroutine(FrontAttack(endPos.position, flightTime, speedRate));

                    break;

                case AttackType.enUpAttack:         //上攻撃のとき。

                    StartCoroutine(ParabolaAttack(AttackType.enUpAttack, endPos.position, flightTime, speedRate, gravity * heightRate));

                    break;

                case AttackType.enRightAttack:      //右攻撃のとき。

                    StartCoroutine(ParabolaAttack(AttackType.enRightAttack, endPos.position, flightTime, speedRate, gravity * heightRate));

                    break;

                case AttackType.enLeftAttack:       //左攻撃のとき。

                    StartCoroutine(ParabolaAttack(AttackType.enLeftAttack, endPos.position, flightTime, speedRate, gravity * heightRate));

                    break;
            }
        }
    }
    /// <summary>
    /// 前方攻撃。
    /// </summary>
    /// <param name="endPos">玉の終着点</param>
    /// <param name="flightTime">敵に玉が当たるまでの時間</param>
    /// <param name="speedRate">玉のスピード倍率</param>
    /// <returns>フレーム</returns>
    public IEnumerator FrontAttack(Vector3 endPos, float flightTime, float speedRate)
    {
        // Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, transform.position, Quaternion.identity);

        var startPos = this.transform.position;                         //初期座標。
        var distance = endPos - startPos;                               //玉までの距離。    
        var processingNum = Time.deltaTime * speedRate / flightTime;    //処理の回数。
        var movementOneFrame = distance * processingNum;                //1Frameあたりの移動量。

        //玉の処理。
        for (var i = 0f; i < 1; i += (Time.deltaTime * speedRate / flightTime))
        {
            ball.transform.position += movementOneFrame;
            yield return null; //1フレーム経過
        }

        ball.transform.position += endPos;
    }

    /// <summary>
    /// 放物線を描く敵の攻撃処理。
    /// </summary>
    /// <param name="attackNum">攻撃タイプ</param>
    /// <param name="endPos">攻撃の終着点</param>
    /// <param name="flightTime">敵に玉が当たるまでの時間</param>
    /// <param name="speedRate">玉のスピード倍率</param>
    /// <param name="gravity">攻撃にかかる重力</param>
    /// <returns></returns>
    public IEnumerator ParabolaAttack(AttackType attackNum, Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        // Ballオブジェクトの生成
        GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);

        var startPos = transform.position; // 初期位置

        var diff = endPos - startPos;      // 始点と終点の成分の差分; 
        var diffX = (endPos - startPos).x; // 始点と終点のx成分の差分
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var diffZ = (endPos - startPos).z; // 始点と終点のz成分の差分

        var vn = (gravity * 0.5f * flightTime * flightTime) / flightTime;          // 鉛直方向の初速度vn
        var vnx = (diffX - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn
        var vny = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vny
        var vnz = (diffZ - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        Vector3 y = new Vector3(0, 1, 0);               //敵の直上ベクトル。
        var crossVec = Vector3.Cross(diff, y);          //外積を使ってプレイヤー方向の垂線を求める。
        var crossVecNormalize = crossVec.normalized;    //正規化。

        //放物線を描く玉の処理。
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)

            switch(attackNum){

                case AttackType.enUpAttack:       //上攻撃。

                    p.y = startPos.y + vny * t + 0.5f * gravity * t * t; // 鉛直方向の座標y

                    break;

                case AttackType.enRightAttack:    //右攻撃のとき。

                    p += crossVecNormalize * (vn * t - 0.5f * gravity * t * t);// 鉛直方向の座標 x

                    break;

                case AttackType.enLeftAttack:     //左攻撃のとき。
                                           
                    p += (-crossVecNormalize) * (vn * t - 0.5f * gravity * t * t);// 鉛直方向の座標 x

                    break;
            }

            ball.transform.position = p;
            yield return null; //1フレーム経過
        }

        // 終点座標へ補正
        ball.transform.position = endPos;
    }
}

