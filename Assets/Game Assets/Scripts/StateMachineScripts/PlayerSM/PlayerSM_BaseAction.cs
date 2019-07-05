using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	public class PlayerSM_BaseAction : ActionTask{

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
        protected virtual void HandleInput()
        { }
         protected virtual void ComputeVelocity()
        { }
        protected virtual void HandleAnimations()
        { }
	}
}