using CommunityToolkit.Mvvm.Messaging.Messages;

public class EntityUpdatedMessage<T> : ValueChangedMessage<T>
{
    public EntityUpdatedMessage(T entity) : base(entity)
    {
    }
}
