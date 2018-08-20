using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Wine.Test.Delegates
{
    public class ExperimentTracker
    {        
        Queue queueList;

        public ExperimentTracker()
        {
            queueList = new Queue();
        }

        public void LoadQueue()
        {
            for (int i = 0; i < 8; i++)
                queueList.Enqueue(i);
        }

        public void FlightTracker()
        {
            while (queueList.Count > 0)
            {
                var r = queueList.Dequeue();
                  
            }
        }

        public void CheckNeighbours()
        {
            for (int i = 0; i < 8; i++)
            {
                if (queueList.Contains(i) == false)
                {
                    queueList.Enqueue(i);
                    queueList.Dequeue();
                }
            }
        }

        public class TreeNode
        {

        }
    }

}
