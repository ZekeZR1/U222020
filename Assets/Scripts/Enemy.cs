using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyAttack enemyAttack;        //攻撃処理クラス。
    public CautionHUD cautionHUD;          //攻撃の危険信号Image。

    private float timer = 0f;             //タイマー。
    private bool isRunning = false;

    private enum PlayerState        //プレイヤーの状態。
    {
        enIdle = 0,         //待機状態。 
        enFrontAttack,      //前方攻撃。
        enUpAttack,         //上攻撃。
        enRightAttack,      //右攻撃。
        enLeftAttack,       //左攻撃。
        enStateNum          //状態の数。
    };

    PlayerState playerState = PlayerState.enStateNum;       //プレイヤーの状態を保持する。

    public void StartShooting()
    {
        isRunning = true;
    }

    public void StopShooting()
    {
        isRunning = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();      //攻撃処理クラスのインスタンスを取得。
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            //todo 仮。
            AttackProcessing();
        }
    }

    //攻撃処理。仮。
    void AttackProcessing()
    {
        timer += Time.deltaTime;    //タイマーを加算。

        if(timer >= 1f)     //約１秒経過した時。
        {           
            var value = Random.value;       //0.0~1.0の乱数を取得。

            if(value <= 0.25)       //前方攻撃。
            {
                playerState = PlayerState.enFrontAttack;
                AttackOrder(EnemyAttack.AttackType.enFrontAttack);
                cautionHUD.NotifyCenterShot();
            }
            else if(value > 0.25 && value <= 0.5)       //上攻撃。
            {
                playerState = PlayerState.enUpAttack;
                AttackOrder(EnemyAttack.AttackType.enUpAttack);
                cautionHUD.NotifyAboveShot();
            }
            else if(value >0.5 && value <= 0.75)        //右攻撃。
            {
                playerState = PlayerState.enRightAttack;
                AttackOrder(EnemyAttack.AttackType.enRightAttack);
                cautionHUD.NotifyRightShot();
            }
            else        //左攻撃。
            {
                playerState = PlayerState.enLeftAttack;
                AttackOrder(EnemyAttack.AttackType.enLeftAttack);
                cautionHUD.NotifyLeftShot();
            }
        }
    }

    /// <summary>
    /// 攻撃命令。
    /// </summary>
    /// <param name="attackNum">攻撃の種類</param>
    void AttackOrder(EnemyAttack.AttackType attackNum)
    {
        enemyAttack.Attack(attackNum);      //攻撃処理。
        timer = 0f;     //タイマーをリセット。
    }
}
