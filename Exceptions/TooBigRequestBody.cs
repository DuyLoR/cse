namespace CSE_website.Exceptions;

public class TooBigRequestBody : Exception
{
    public TooBigRequestBody() { }
    public TooBigRequestBody(string message) : base(message) { }
    public TooBigRequestBody(string message, Exception inner) : base(message, inner) { }
    protected TooBigRequestBody(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}