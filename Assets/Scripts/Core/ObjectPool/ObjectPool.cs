﻿using System;
using System.Collections.Generic;

namespace Core.ObjectPool {
    public class ObjectPool<T> {
        readonly List<T> _currentStock;
        readonly Func<T> _factoryMethod;

        readonly bool _isDynamic;

        readonly Action<T> _turnOnCallback;
        readonly Action<T> _turnOffCallback;

        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback,
            int initialStock = 0, bool isDynamic = true) {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = new List<T>();

            for (var i = 0; i < initialStock; i++) {
                var instance = _factoryMethod();
                _turnOffCallback(instance);
                _currentStock.Add(instance);
            }
        }

        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback,
            List<T> initialStock, bool isDynamic = true) {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = initialStock;
        }

        public T GetObject() {
            var result = default(T);
            if ( _currentStock.Count > 0 ) {
                result = _currentStock[0];
                _currentStock.RemoveAt(0);
            }
            else if ( _isDynamic ) {
                result = _factoryMethod();
            }

            _turnOnCallback(result);
            return result;
        }

        public void ReturnObject(T obj) {
            _turnOffCallback(obj);
            _currentStock.Add(obj);
        }
    }
}