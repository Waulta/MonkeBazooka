using System;
using ComputerInterface.Interfaces;

namespace MonkeBazooka.ComputerInterface
{
    class MonkeBazookaEntry : IComputerModEntry
    {
        public string EntryName => "Monke Bazooka";

        // This is the first view that is going to be shown if the user select you mod
        // The Computer Interface mod will instantiate your view 
        public Type EntryViewType => typeof(MonkeBazookaView);
    }
}