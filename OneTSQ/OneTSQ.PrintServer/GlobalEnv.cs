using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.PrintServer
{
    public enum ETableStatus
    {
        empty,
        checkin,
        repaired,
        reserved
    }

    public class GlobalEnv
    {
        public static EventProcess StaticEventProcess = new EventProcess();
    }


    public class EventProcess
    {
        public delegate void UpdateTotalHandler(string TransactionId);
        public event UpdateTotalHandler UpdateTotalEvent;


        public void RaiseUpdateTotalEvent(string TransactionId)
        {
            if (UpdateTotalEvent != null)
            {
                UpdateTotalEvent(TransactionId);
            }
        }

    }

    
}
