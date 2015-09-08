﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;

namespace Perspex.Styling.UnitTests
{
    internal class TestObserver<T> : IObserver<T>
    {
        private bool _hasValue;

        private T _value;

        public bool Completed { get; private set; }

        public Exception Error { get; private set; }

        public T GetValue()
        {
            if (!_hasValue)
            {
                throw new Exception("Observable provided no value.");
            }

            if (this.Completed)
            {
                throw new Exception("Observable completed unexpectedly.");
            }

            if (this.Error != null)
            {
                throw new Exception("Observable errored unexpectedly.");
            }

            _hasValue = false;
            return _value;
        }

        public void OnCompleted()
        {
            this.Completed = true;
        }

        public void OnError(Exception error)
        {
            this.Error = error;
        }

        public void OnNext(T value)
        {
            if (!_hasValue)
            {
                _value = value;
                _hasValue = true;
            }
            else
            {
                throw new Exception("Observable pushed more than one value.");
            }
        }
    }
}
