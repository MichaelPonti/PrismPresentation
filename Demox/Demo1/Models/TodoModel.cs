using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Models
{
	public class TodoModel
	{
		public int Id { get; set; }
		public string Headline { get; set; }
		public string Description { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? DueDate { get; set; }
		public string AssignedTo { get; set; }
		public bool IsComplete { get; set; }

		public TodoModel()
		{
		}

		public TodoModel(int id, string headline, string description, DateTime? createDate, DateTime? dueDate, string assignedTo, bool isComplete)
		{
			Id = id;
			Headline = headline;
			Description = description;
			CreateDate = createDate;
			DueDate = dueDate;
			AssignedTo = assignedTo;
			IsComplete = isComplete;
		}
	}
}
