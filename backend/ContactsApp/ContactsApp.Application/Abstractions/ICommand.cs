using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Abstractions
{
    public interface ICommand 
    {
    }

    public interface ICommand<TResponse>
    {
    }
}
