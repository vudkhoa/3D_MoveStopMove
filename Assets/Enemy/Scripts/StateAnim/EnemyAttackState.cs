using Utils.DesignPattern.State;
using Enemy.View;
using UnityEngine;

namespace Enemy.StateAnim
{
    public class EnemyAttackState : IState<EnemyView>
    {
        public void Enter(EnemyView enemy) 
        {
            enemy.animator.SetBool("IsAttacking", true);
        }

        public void Execute(EnemyView enemy) 
        { 
            if (!enemy.IsAttacking)
            {
                if (enemy.IsRunning)
                {
                    enemy.StateMachine.Change(enemy, new EnemyRunningState());
                }
                else if (enemy.IsIdle)
                {
                    enemy.StateMachine.Change(enemy, new EnemyIdleState());
                }
            }
        }

        public void Exit(EnemyView enemy) 
        { 
            enemy.animator.SetBool("IsAttacking", false);
        }
    }
}
