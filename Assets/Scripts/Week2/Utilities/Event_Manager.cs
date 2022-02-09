using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{
    private Dictionary<Type, AGPEvent.Handler> _registeredHandlers = new Dictionary<Type, AGPEvent.Handler>();

	public void Register<T>(AGPEvent.Handler handler) where T : AGPEvent
	{
		var type = typeof(T);
		//Check dictionary already have this event type
		if (_registeredHandlers.ContainsKey(type))
		{
			if (!IsEventHandlerRegistered(type, handler))
				_registeredHandlers[type] += handler;
		}
		else
		{
			//If not, generate new type as key, assign handler as value
			_registeredHandlers.Add(type, handler);
		}
	}

	public void Unregister<T>(AGPEvent.Handler handler) where T : AGPEvent
	{
		var type = typeof(T);
		if (!_registeredHandlers.TryGetValue(type, out var handlers)) return;

		handlers -= handler;

		if (handlers == null)
		{
			_registeredHandlers.Remove(type);
		}
		else
		{
			_registeredHandlers[type] = handlers;
		}
	}
	public void Fire(AGPEvent e)
	{
		var type = e.GetType();

		if (_registeredHandlers.TryGetValue(type, out var handlers))
		{
			handlers(e);
		}
	}

	public bool IsEventHandlerRegistered(Type typeIn, Delegate prospectiveHandler)
	{
		return _registeredHandlers[typeIn].GetInvocationList().Any(existingHandler => existingHandler == prospectiveHandler);
	}
}
public abstract class AGPEvent
{
	public readonly float creationTime;

	public AGPEvent()
	{
		creationTime = Time.time;
	}

	public delegate void Handler(AGPEvent e);
}

public class Event_OnScore : AGPEvent
{
	public readonly int teamIDScored;

	public Event_OnScore(int teamIDScored)
	{
		this.teamIDScored = teamIDScored;
	}
}

public class Event_OnGenerateCube : AGPEvent
{

}

public class Event_OnGameStart : AGPEvent
{

}

public class Event_OnTimeUp : AGPEvent
{
	public readonly int Score_Red;
	public readonly int Score_Blue;

	public Event_OnTimeUp(int Score_red, int Score_blue)
	{
		this.Score_Red = Score_red;
		this.Score_Blue = Score_blue;
	}
}
