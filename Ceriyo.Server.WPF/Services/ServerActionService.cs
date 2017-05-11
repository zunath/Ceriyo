using System;
using System.Collections.Concurrent;
using Ceriyo.Server.WPF.Actions;
using Ceriyo.Server.WPF.Contracts;

namespace Ceriyo.Server.WPF.Services
{
    public class ServerActionService: IServerActionService
    {
        private readonly ConcurrentQueue<IServerAction> _actionQueue;

        public ServerActionService()
        {
            _actionQueue = new ConcurrentQueue<IServerAction>();
        }

        public void QueueAction(IServerAction action)
        {
            _actionQueue.Enqueue(action);
        }

        public void ProcessActions()
        {
            while (!_actionQueue.IsEmpty)
            {
                IServerAction action;
                if (_actionQueue.TryDequeue(out action))
                {
                    action.Process();
                }
            }
        }
    }
}
