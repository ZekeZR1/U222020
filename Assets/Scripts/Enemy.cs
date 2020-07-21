using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyAttack enemyAttack;        //攻撃処理クラス。

    private float timer = 0f;             //タイマー。

    // Start is called before the first frame update
    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();      //攻撃処理クラスのインスタンスを取得。
    }

    // Update is called once per frame
    void Update()
    {
        //todo 仮。
        AttackProcessing();
    }

    //攻撃処理。仮。
    void AttackProcessing()
    {
        var attackNum = enemyAttack.attackType;     //攻撃方法。

        timer += Time.deltaTime;    //タイマーを加算。

        if(timer >= 1f)     //約１秒経過した時。
        {           
            var value = Random.value;       //0.0~1.0の乱数を取得。

            if(value <= 0.25)       //前方攻撃。
            {
                attackNum = EnemyAttack.AttackType.enFrontAttack;
            }
            else if(value > 0.25 && value <= 0.5)       //上攻撃。
            {
                attackNum = EnemyAttack.AttackType.enUpAttack;
            }
            else if(value >0.5 && value <= 0.75)        
            {
                attackNum = EnemyAttack.AttackType.enRightAttack;
            }
            else        //左攻撃。
            {
                attackNum = EnemyAttack.AttackType.enLeftAttack;
            }

            enemyAttack.Attack(attackNum);

            timer = 0f;     //タイマーをリセット。
        }
    }
}
