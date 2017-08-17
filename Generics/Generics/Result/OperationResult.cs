using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Result
{
    public class OperationResult<TResult>
    {
        private OperationResult()
        {
        }
        public bool Success { get; private set; }
        public TResult Result { get; private set; }
        public string NonSuccessMessage { get; private set; }
        public Exception Exception { get; private set; }
        public static OperationResult<TResult> CreateSuccessResult(TResult result)
        {
            return new OperationResult<TResult> { Success = true, Result = result };
        }
        public static OperationResult<TResult> CreateFailure(string nonSuccessMessage)
        {
            return new OperationResult<TResult> { Success = false, NonSuccessMessage = nonSuccessMessage };
        }
        public static OperationResult<TResult> CreateFailure(Exception ex)
        {
            return new OperationResult<TResult>
            {
                Success = false,
                NonSuccessMessage = String.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace),
                Exception = ex
            };
        }
    }
}
