using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace KseF.Models.ViewModels
{
    public class MyBusinessEntityChangedMessage : ValueChangedMessage<MyBusinessEntities>
    {
        public MyBusinessEntityChangedMessage(MyBusinessEntities entity) : base(entity)
        {
        }
    }
}
