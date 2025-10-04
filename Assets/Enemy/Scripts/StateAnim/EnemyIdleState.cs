using Enemy.View;
using Utils.DesignPattern.State;

namespace Enemy.StateAnim
{
    public class EnemyIdleState : IState<EnemyView>
    {
        public void Enter(EnemyView enemy) 
        {
            enemy.animator.SetBool("IsIdle", true);
        }

        public void Execute(EnemyView enemy)
        {
            if (!enemy.IsIdle)
            {
                if (enemy.IsRunning)
                {
                    enemy.StateMachine.Change(enemy, new EnemyRunningState());
                }
                else if (enemy.IsAttacking)
                {
                    enemy.StateMachine.Change(enemy, new EnemyAttackState());
                }
            }
        }

        public void Exit(EnemyView enemy) 
        {
            enemy.animator.SetBool("IsIdle", false);
        }
    }
}