using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EventBus {
	public abstract class HandlerBase {
	}
	
	public sealed class Handler<T> : HandlerBase {
		readonly List<Action<T>> _actions = new List<Action<T>>(100);

		public void Subscribe(object watcher, Action<T> action) {
			if ( !_actions.Contains(action) ) {
				_actions.Add(action);
			} else {
				Debug.LogWarningFormat("{0} tries to subscribe to {1} again.", watcher, action);
			}
		}

		public void Unsubscribe(Action<T> action) {
			if ( _actions.Contains(action) ) {
				_actions.Remove(action);
			}
		}

		public void Fire(T arg) {
			for (var i = 0; i < _actions.Count; i++) {
				var current = _actions[i];

				try {
					current.Invoke(arg);
				}
				catch ( Exception e ) {
					Debug.Log($"Can't invoke event: {typeof(T)}, {e}");
				}
			}
			
		}
	}
}