using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KseF.Models.ViewModels
{
    internal class EntityDeletedMessage<T> : ValueChangedMessage<T>
    {
        public EntityDeletedMessage(T entity) : base(entity)
        {
        }
    }
}
