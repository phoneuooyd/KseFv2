using CommunityToolkit.Mvvm.Messaging.Messages;

namespace KseF.Models.ViewModels
{
    internal class MessageSender<T> : ValueChangedMessage<T>
    {
        public MessageSender(T entity) : base(entity)
        {

        }
    }
}
