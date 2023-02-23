using Sight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class State : MonoBehaviour
    {
        [Header("State parameter")]
        public LineOfSight lineSight;
        public State nextState;


        private void Start()
        {
            lineSight = GetComponent<LineOfSight>();
        }


        /// <summary>
        /// Enters the actual State.
        /// </summary>
        public virtual void Enter()
        {
            Debug.Log("Enter ");
        }

        /// <summary>
        /// Updates the state.
        /// </summary>
        public virtual void UpdateState()
        {
            Debug.Log("Update ");
        }

        /// <summary>
        /// Exits the actual State.
        /// </summary>
        public virtual void Exit()
        {
            Debug.Log("Exit ");
        }
    }
}

