using System;
using ComputerInterface.Interfaces;

namespace MonkeBazooka.ComputerInterface
{
    class MonkeBazookaEntry : IComputerModEntry
    {
        public string EntryName => "Monke Bazooka";

        public Type EntryViewType => typeof(MonkeBazookaView);
    }
}