using Utils.DesignPattern.State;
using Enemy.View;

namespace Enemy.StateAnim
{
    public class EnemyAttackState : IState<EnemyView>
    {
        public void Enter(EnemyView enemy) { }

        public void Execute(EnemyView enemy) { }

        public void Exit(EnemyView enemy) { }
    }
}
