using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	public class PlayerSM_BaseAction : ActionTask{

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected PlayerController player_script;

       protected void initializeBaseTask()
        {
            player_script = agent.GetComponent<PlayerController>();
        }

        protected override string OnInit()
        {
            initializeBaseTask();
            return null;
        }

        protected virtual void HandleInput()
        { }
         protected virtual void ComputeVelocity()
        { }
        protected virtual void HandleAnimations()
        { }
        protected virtual void SpecificOnInit()
        {

        }

        protected override void OnExecute()
        {
            EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            HandleInput();
            ComputeVelocity();
            HandleAnimations();
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}