using NodeCanvas.Framework;
using ParadoxNotion;

using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions{

    [Category("✫ Blackboard")]
    public class CheckVectorX : ConditionTask{

        [BlackboardOnly]
        public BBParameter<Vector2> vector;
        [BlackboardOnly]
        public BBParameter<float> objective_value;
        [BlackboardOnly]
        public CompareMethod comparison;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck(){
            var vec_x = Mathf.Abs(vector.value.x);
            var _magnitude = objective_value.value;
            switch (comparison)
            {
                case CompareMethod.EqualTo:
                    return vec_x == _magnitude ? true : false;
                    break;
                case CompareMethod.GreaterOrEqualTo:
                    return vec_x >= _magnitude ? true : false;
                    break;
                case CompareMethod.GreaterThan:
                    return vec_x > _magnitude ? true : false;
                    break;
                case CompareMethod.LessOrEqualTo:
                    return vec_x <= _magnitude ? true : false;
                    break;
                case CompareMethod.LessThan:
                    return vec_x < _magnitude ? true : false;
            }
			return true;
		}
	}
}