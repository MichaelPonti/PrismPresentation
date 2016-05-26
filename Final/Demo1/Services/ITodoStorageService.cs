using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Services
{
	public interface ITodoStorageService
	{
		Task<List<TodoModel>> GetListAsync();
		Task<TodoModel> GetAsync(int id);
		Task<bool> AddAsync(TodoModel m);
		Task<bool> DeleteAsync(int id);
		Task<bool> UpdateAsync(TodoModel m);
	}
}
