using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EventBus {
	public abstract class HandlerBase {
	}
	
	public sealed class Handler<T> : HandlerBase {
		List<Action<T>> _actions = new List<Action<T>>(100);
		List<Action<T>> _removed = new List<Action<T>>(100);

		public void Subscribe(object watcher, Action<T> action) {
			if ( _removed.Contains(action) ) {
				_removed.Remove(action);
			}

			if ( !_actions.Contains(action) ) {
				_actions.Add(action);
			} else {
				Debug.LogWarningFormat("{0} tries to subscribe to {1} again.", watcher, action);
			}
		}

		public void Unsubscribe(Action<T> action) {
			var index = _actions.IndexOf(action);
			if ( index >= 0 ) {
				_removed.Add(_actions[index]);
			}
		}

		public void Fire(T arg) {
			for (var i = 0; i < _actions.Count; i++) {
				var current = _actions[i];
				if ( _removed.Contains(current) ) {
					continue;
				}

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