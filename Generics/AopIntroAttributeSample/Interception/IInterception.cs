using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Interception
{
    public interface IInterception
    {
    }

    public interface IPostInterception : IInterception
    {
        object OnPost(PostInterceptArgs e);
    }

    public interface IPostVoidInterception : IInterception
    {
        void OnPost(PostInterceptArgs e);
    }
    public interface IPreInterception : IInterception
    {
        object OnPre(PreInterceptArgs e);
    }
    public interface IPreVoidInterception : IInterception
    {
        void OnPre(PreInterceptArgs e);
    }
    public interface IExceptionInterception : IInterception
    {
        void OnException(ExceptionInterceptArgs e);
    }

    public abstract class InterceptAttribute : Attribute, IInterception
    {

    }

    public class InterceptArgs : EventArgs
    {
        public string MethodName { get; protected set; }
        public object[] Arguments { get; protected set; }

        public InterceptArgs(string methodName, object[] arguments)
        {
            MethodName = methodName;
            Arguments = arguments;
        }

        public InterceptArgs(InterceptArgs e)
            : this(e.MethodName, e.Arguments)
        {

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Method : {0}", MethodName));
            sb.Append(string.Format(" Args : {0}", string.Join(",", Arguments)));
            return sb.ToString();
        }
    }

    public class PreInterceptArgs : InterceptArgs
    {
        public PreInterceptArgs(string methodName, object[] arguments)
            : base(methodName, arguments)
        {
        }

        public PreInterceptArgs(InterceptArgs e) : base(e) { }

    }

    public class PostInterceptArgs : InterceptArgs
    {

        public object Value { get; protected set; }

        public PostInterceptArgs(string methodName, object[] arguments)
            : base(methodName, arguments)
        {
        }

        public PostInterceptArgs(InterceptArgs e) : base(e) { }

        public PostInterceptArgs(InterceptArgs e, object val) : this(e)
        {
            Value = val;
        }
    }

    public class ExceptionInterceptArgs : InterceptArgs
    {

        public Exception Ex { get; set; }

        public ExceptionInterceptArgs(string methodName, object[] arguments)
            : base(methodName, arguments)
        {
        }

        public ExceptionInterceptArgs(InterceptArgs e) : base(e) { }

        public ExceptionInterceptArgs(InterceptArgs e, Exception ex)
            : this(e)
        {
            Ex = ex;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(string.Format(" Ex : {0}", Ex.ToString()));
            return sb.ToString();
        }
    }
}
