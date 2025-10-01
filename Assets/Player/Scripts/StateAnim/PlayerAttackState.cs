using Player.View;
using Utils.DesignPattern.State;

namespace Player.StateAnim
{
    public class PlayerAttackState : IState<PlayerView>
    {
        public void Enter(PlayerView player) { }
        public void Execute(PlayerView player) { }
        public void Exit(PlayerView player) { }
    }
}

