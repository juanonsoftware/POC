using Stateless;

namespace StateDemoConsole
{
    public class NewArticleStateManager
    {
        private readonly StateMachine<State, Action> _stateMachine;

        public NewArticleStateManager(NewArticle article)
        {
            _stateMachine = new StateMachine<State, Action>(
                () => article.State,
                s => article.State = s);

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