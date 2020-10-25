using Stateless;

namespace StateDemoConsole
{
    public class Article
    {
        private readonly StateMachine<State, Action> _stateMachine;

        public State State { get; set; }

        public Article(string title, string description)
        {
            _stateMachine = new StateMachine<State, Action>(
                () => State,
                s => State = s);

            _stateMachine.Configure(State.New)
                .Permit(Action.Validate, State.Validated)
                .PermitReentry(Action.Edit)
                .Permit(Action.Delete, State.Deleted);

            _stateMachine.Configure(State.Edited)
                .Permit(Action.Validate, State.Validated)
                .Permit(Action.Delete, State.Deleted)
                .PermitReentry(Action.Edit);

            _stateMachine.Configure(State.Validated)
                .Permit(Action.Edit, State.Edited)
                .Permit(Action.Delete, State.Deleted)
                .Ignore(Action.Validate);

            _stateMachine.Configure(State.Deleted)
                .Permit(Action.Restore, State.Edited)
                .Ignore(Action.Delete);
        }

        public void Perform(Action action)
        {
            _stateMachine.Fire(action);
        }
    }
}