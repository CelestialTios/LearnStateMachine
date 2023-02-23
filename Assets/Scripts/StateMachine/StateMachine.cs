using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        public State currentState;

        // Start is called before the first frame update
        void Start()
        {
            currentState.Enter();
        }

        // Update is called once per frame
        void Update()
        {
            currentState.UpdateState();

            if(currentState.nextState != null)
            {
                ChangeState(currentState.nextState);
            }
        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ChangeState(State newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}
