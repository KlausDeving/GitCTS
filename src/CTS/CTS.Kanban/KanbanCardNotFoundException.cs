using System;
using System.Runtime.Serialization;

namespace CTS.Kanban;
[Serializable]
internal class KanbanCardNotFoundException : Exception
{
    public KanbanCardNotFoundException()
    {
    }

    public KanbanCardNotFoundException(string? message) : base(message)
    {
    }

    public KanbanCardNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected KanbanCardNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}