using System;

namespace Warden.Common.Commands
{
    //Marker interface
    public interface ICommand
    {
        Request Request { get; set; }
    }
}