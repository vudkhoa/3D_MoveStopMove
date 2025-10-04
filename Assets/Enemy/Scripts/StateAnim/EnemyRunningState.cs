using Enemy.View;
using Utils.DesignPattern.State;
using UnityEngine;
namespace Enemy.StateAnim
{
    public class EnemyRunningState : IState<EnemyView>
    {
        public void Enter(EnemyView enemy)
        {
            enemy.animator.SetBool("IsRunning", true);
        }

        public void Execute(EnemyView enemy)
        {
            if (!enemy.IsRunning)
            {
                if (enemy.IsAttacking)
                {
                    enemy.StateMachine.Change(enemy, new EnemyAttackState());
                }
                else if (enemy.IsIdle)
                {
                    enemy.StateMachine.Change(enemy, new EnemyIdleState());
                }
            }
        }

        public void Exit(EnemyView enemy)
        {
            enemy.animator.SetBool("IsRunning", false);
        }
    }
}