using Demo1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Demo1.Services
{
	public class LocalStorageService : ITodoStorageService
	{
		private DataContainer _dataContainer = null;
		private string _nominalFilename = null;

		public LocalStorageService(string nominalFilename = null)
		{
			_nominalFilename = (String.IsNullOrWhiteSpace(nominalFilename))
				? "todos.json"
				: nominalFilename;

			_dataContainer = new DataContainer();
		}


		public async Task Load()
		{
			if (_dataContainer == null)
				_dataContainer = new DataContainer();
			_dataContainer.Todos.Clear();

			try
			{
				StorageFolder localFolder = ApplicationData.Current.LocalFolder;
				StorageFile todoItemsFile = await localFolder.GetFileAsync(_nominalFilename);
				string fileContent = await FileIO.ReadTextAsync(todoItemsFile);
				if (!String.IsNullOrWhiteSpace(fileContent))
				{
					_dataContainer = Newtonsoft.Json.JsonConvert.DeserializeObject<DataContainer>(fileContent);
				}
			}
			catch (FileNotFoundException)
			{
			}
		}

		public async Task Save()
		{
			if (_dataContainer == null)
				_dataContainer = new DataContainer();

			try
			{
				StorageFolder localFolder = ApplicationData.Current.LocalFolder;
				StorageFile todoItemsFiles = await localFolder.CreateFileAsync(_nominalFilename, CreationCollisionOption.ReplaceExisting);
				string fileContents = Newtonsoft.Json.JsonConvert.SerializeObject(_dataContainer);
				await FileIO.WriteTextAsync(todoItemsFiles, fileContents);
			}
			catch (Exception)
			{
			}
		}


		private int GetNextId()
		{
			int nextId = 0;
			if (_dataContainer.Todos.Count > 0)
			{
				nextId = _dataContainer.Todos.Max(m => m.Id);
			}

			return (nextId + 1);
		}


		public Task<bool> AddAsync(TodoModel m)
		{
			int nextId = GetNextId();
			m.Id = nextId;
			_dataContainer.Todos.Add(m);
			return Task.FromResult<bool>(true);
		}

		public Task<bool> DeleteAsync(int id)
		{
			var toDelete = (from item in _dataContainer.Todos
							where item.Id == id
							select item)
							.FirstOrDefault();

			if (toDelete == null)
				return Task.FromResult<bool>(false);

			_dataContainer.Todos.Remove(toDelete);
			return Task.FromResult<bool>(true);
		}

		public Task<TodoModel> GetAsync(int id)
		{
			var item = (from t in _dataContainer.Todos
						where t.Id == id
						select t)
						.FirstOrDefault();

			return Task.FromResult<TodoModel>(item);
		}

		public Task<List<TodoModel>> GetListAsync()
		{
			List<TodoModel> ret = new List<TodoModel>();
			foreach (var item in _dataContainer.Todos)
			{
				ret.Add(new TodoModel(
					item.Id,
					item.Headline,
					item.Description,
					item.CreateDate,
					item.DueDate,
					item.AssignedTo,
					item.IsComplete));
			}

			return Task.FromResult<List<TodoModel>>(ret);
		}

		public Task<bool> UpdateAsync(TodoModel m)
		{
			var t = (from i in _dataContainer.Todos
					 where i.Id == m.Id
					 select i)
					 .FirstOrDefault();

			if (t == null)
				return Task.FromResult<bool>(false);

			t.AssignedTo = m.AssignedTo;
			t.CreateDate = m.CreateDate;
			t.Description = m.Description;
			t.DueDate = m.DueDate;
			t.Headline = m.Headline;
			t.IsComplete = m.IsComplete;

			return Task.FromResult<bool>(true);
		}
	}
}
