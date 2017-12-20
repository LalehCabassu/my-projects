using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tasks
{
    public class Task
    {
        private static int nextId = 0;

        private int id;

        public int Id { get { return id; } }
        public Client Client { get; set; }      //used this task
        public int ClientId { get; set; }
        public string DocumentType { get; set; }
        public int TotalSize { get; set; }
        public int SegmentSize { get; set; }
        public byte[][] Segment { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime FinishedOn { get; set; }

        public Task(Client client, int clientId, string documentType, int totalSize, int segmentSize)
        {
            id = nextId++;
        }
    }
}
