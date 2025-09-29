using Player.View;
using Utils.DesignPattern.State;
using Player.StateAnim;
using UnityEngine;

public class PlayerIdleState : IState<PlayerView>
{
    public void Enter(PlayerView player) { }

    public void Execute(PlayerView player)
    {
        //Debug.Log("Player is Idle");
        if (player.IsMoving())
        {
            player.StateMachine.Change(player, new PlayerRunningState());
        }
    }

    public void Exit(PlayerView player) { }
}
