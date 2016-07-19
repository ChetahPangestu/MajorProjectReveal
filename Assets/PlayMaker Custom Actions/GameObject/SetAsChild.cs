// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
//Made by Sly for Playmaker and Playmaker community
//Action used to set a gameobject the child of the choosen parent

// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Split a string to two variable, regarding a separator.")]
	public class SetAsChild : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Parent GameObject")]
		public FsmOwnerDefault ChildObject;

		[RequiredField]
		[Tooltip("GameObject to set as child")]
		public FsmGameObject ParentObject;

		public override void OnEnter()
		 {
			if (ChildObject.GameObject.Value == null || ParentObject.Value == null)
			{
				LogWarning("GameObject Child or Parent (or both) are null! Please fix it!");
			}
			else
			{
				//Makes the GameObject "ParentObject" the parent of the GameObject "ChildObject".
			ChildObject.GameObject.Value.transform.parent = ParentObject.Value.gameObject.transform;
			}

			Finish();

		}
		
	}
		
		
}
