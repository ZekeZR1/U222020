using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyAttack enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        //enemyAttack = new EnemyAttack();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //todo 仮。
        enemyAttack.Attack(0);
    }

    //攻撃処理。仮。
    void AttackProcessing()
    {

        var attackNum = 0;

        var timer = 0f;             //タイマー。
        timer += Time.deltaTime;    //タイマーを加算。

        if(timer >= 1f)     //約１秒経過した時。
        {
            
            var value = Random.value;
            if(value <= 0.25)
            {
                attackNum = 0;
            }
            else if(value > 0.25 && value <= 0.5)
            {
                attackNum = 1;
            }
            else if(value >0.5 && value <= 0.75)
            {
                attackNum = 2;
            }
            else
            {
                attackNum = 3;
            }

        //    enemyAttack.Attack(attackNum);

            timer = 0f;
        }

    }
}
