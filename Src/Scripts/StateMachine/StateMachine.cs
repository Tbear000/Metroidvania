using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;


/// Generic state machine. Initializes states and delegates engine callbacks

public class StateMachine : Node
{
	[Signal]
	public delegate void Transitioned(string stateName);

	/// Path to the initial active state. We export it to be able to pick the initial state in the inspector.
	[Export]
	public NodePath InitialState;

	public State State;

	public override void _Ready()
	{
		State = GetNode<State>(InitialState);

		//Task.Run(async () => await ToSignal(Owner, "ready"));

		// The state machine assigns itself to the State objects' state_machine property.
		foreach (State child in GetChildren())
		{
			child._stateMachine = this;
		}

		State.Enter();
	}

	/// The state machine subscribes to node callbacks and delegates them to the state objects.
	public override void _UnhandledInput(InputEvent @event)
	{
		State.HandleInputs(@event);
	}

	public override void _Process(float delta)
	{
		State.Update(delta);
	}

	public override void _PhysicsProcess(float delta)
	{
		State.PhysicsUpdate(delta);
	}

	/// This function calls the current state's exit() function, then changes the active state,
	/// and calls its enter function.
	/// It optionally takes a `msg` dictionary to pass to the next state's enter() function.

	public void TransitionTo(string targetStateName, Dictionary<string, bool> message = null)
	{
		//  Safety check, you could use an assert() here to report an error if the state name is incorrect.
		//  We don't use an assert here to help with code reuse. If you reuse a state in different state machines
		//  but you don't want them all, they won't be able to transition to states that aren't in the scene tree.
		if (!HasNode(targetStateName))
		{
			return;
		}

		State.Exit();
		State = GetNode<State>(targetStateName);
		State.Enter(message);
		EmitSignal(nameof(Transitioned), State.Name);
	}
}
