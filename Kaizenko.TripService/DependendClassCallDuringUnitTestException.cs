﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Kaizenko.TripService
{
    [Serializable]
    public class DependendClassCallDuringUnitTestException : System.Exception
    {
        public DependendClassCallDuringUnitTestException() : base() { }

        public DependendClassCallDuringUnitTestException(string message, System.Exception innerException) : base(message, innerException) { }

        public DependendClassCallDuringUnitTestException(string message) : base(message) { }

        private DependendClassCallDuringUnitTestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
