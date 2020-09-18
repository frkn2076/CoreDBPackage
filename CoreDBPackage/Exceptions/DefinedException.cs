using System;

namespace CoreDBPackage.Exceptions {
    public class DefinedException : Exception {
        public DefinedException(string message, Exception inner) : base(message, inner) {

        }
    }
}
