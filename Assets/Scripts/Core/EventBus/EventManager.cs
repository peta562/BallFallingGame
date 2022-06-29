using System;
using System.Collections.Generic;

namespace Core.EventBus {
    public sealed class EventManager : Singleton<EventManager> {
        readonly Dictionary<Type, HandlerBase> _handlers = new Dictionary<Type, HandlerBase>();

        public void Subscribe<T>(object watcher, Action<T> action) where T : struct {
            var tHandler = GetOrCreateHandler<T>();
            if ( tHandler != null ) {
                tHandler.Subscribe(watcher, action);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : struct {
            if ( !_handlers.TryGetValue(typeof(T), out var handler) ) {
                return;
            }

            if ( handler is Handler<T> tHandler ) {
                tHandler.Unsubscribe(action);
            }
        }

        public void Fire<T>(T args) where T : struct {
            var tHandler = GetOrCreateHandler<T>();
            tHandler?.Fire(args);
        }

        Handler<T> GetOrCreateHandler<T>() {
            if ( !_handlers.TryGetValue(typeof(T), out var handler) ) {
                handler = new Handler<T>();
                _handlers.Add(typeof(T), handler);
            }

            return handler as Handler<T>;
        }
    }
}