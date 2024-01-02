using System.Collections.Generic;


namespace SamEbac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, Statebase> dictionaryState;

        private Statebase _currentState;
        public float timeToStartGame = 1f;


        public Statebase CurrentSate
        {
            get { return _currentState; }
        }

        //public StateMachine(T state)
        //{
        //    SwitchState(state);
        //}


        public void Init()
        {
            dictionaryState = new Dictionary<T, Statebase>();
        }

        public void RegisterStates(T typeEnum, Statebase state)
        {
            dictionaryState.Add(typeEnum, state);
        }


        public void SwitchState(T state,params object[] objects)
        {
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = dictionaryState[state];

            _currentState.OnStateEnter(objects);
        }

        private void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();

        }


    }

}


