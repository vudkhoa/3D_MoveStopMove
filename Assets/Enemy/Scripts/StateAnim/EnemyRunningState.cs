using Enemy.View;
using Utils.DesignPattern.State;
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
                enemy.StateMachine.Change(enemy, new EnemyIdleState());
            }
        }

        public void Exit(EnemyView enemy)
        {
            enemy.animator.SetBool("IsRunning", true);
        }
    }
}