using System.Runtime.Serialization;

namespace UserContacts.Core.Errors;

public class UnAuthorizedException : BaseException
{
    public UnAuthorizedException() { }
    public UnAuthorizedException(String message) : base(message) { }
    public UnAuthorizedException(String message, Exception inner) : base(message, inner) { }
    protected UnAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
