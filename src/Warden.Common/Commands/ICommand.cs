using System;

namespace Warden.Common.Commands
{
    //Marker interface
    public interface ICommand
    {
        CommandDetails Details { get; set; }
    }
}