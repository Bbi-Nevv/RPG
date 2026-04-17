using UnityEngine;

public interface IState 
{
    void Enter(); // call 1 frame when state is changed
    void Execute(); // call every frame when state is active
    void Exit(); // call 1 frame when state is changed

}


