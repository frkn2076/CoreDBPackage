using CoreDBPackage.Config;
using System;
using System.Collections.Generic;

namespace CoreDBPackage.Exceptions {

    #region NotFound

    public class NotFoundException : Exception {
        public NotFoundException(string message) : base(message, new KeyNotFoundException()) {

        }
    }

    public class DefinedEmailException : NotFoundException {
        public DefinedEmailException() : base(MyCache.getSetting("ErrorDescription")) {

        }
    }

    public class WrongEmailKeyException : NotFoundException {
        public WrongEmailKeyException() : base(MyCache.getSetting("WrongEmail")) {

        }
    }

    #endregion
}
