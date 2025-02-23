using Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
  public class TaskRepositorie : IRepositorie<Domain.Task>
  {
    private IMongoCollection<Domain.Task> _tasks;
    public TaskRepositorie()
    {
      IMongoClient mongoClient = new MongoClient("mongodb+srv://Licenta2025:Licenta2025@cluster0.0fh6p.mongodb.net/");
      IMongoDatabase database = mongoClient.GetDatabase("ToDoDB");
      IMongoCollection<Domain.Task> tasksCollection = database.GetCollection<Domain.Task>("Tasks");
      this._tasks = tasksCollection;
    }
    public async Task<string> CreateAsync(Domain.Task entity)
    {
      entity.Id = ObjectId.GenerateNewId().ToString();
      await this._tasks.InsertOneAsync(entity);
      return entity.Id;
    }

    public async Task<List<Domain.Task>> GetTasksForUserAsync(string userId)
    {
      var filter = Builders<Domain.Task>.Filter.Eq(x => x.UserId, userId);
      var tasks = await this._tasks.Find(filter).ToListAsync();
      return tasks;
    }

    public async Task<bool> MarkTaskAsCompleted(string taskId)
    {
      try
      {
        var filter = Builders<Domain.Task>.Filter.Eq(x => x.Id, taskId);
        var update = Builders<Domain.Task>.Update.Set(x => x.IsCompleted, true);
        await this._tasks.UpdateOneAsync(filter, update);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
      var filter = Builders<Domain.Task>.Filter.Eq(x => x.Id, id);
      await this._tasks.DeleteOneAsync(filter);
      return true;
    }

    public async Task<List<Domain.Task>> GetAllAsync()
    {
      var filter = Builders<Domain.Task>.Filter.Empty;
      var tasks = await this._tasks.Find(filter).ToListAsync();
      return tasks;
    }

    public async Task<Domain.Task> GetByIdAsync(string id)
    {
      var filter = Builders<Domain.Task>.Filter.Eq(x => x.Id, id);
      var task = await this._tasks.Find(filter).FirstOrDefaultAsync();
      return task;
    }

    public async Task<bool> UpdateAsync(Domain.Task entity)
    {
      var filter = Builders<Domain.Task>.Filter.Eq(x => x.Id, entity.Id);
      await this._tasks.ReplaceOneAsync(filter, entity);
      return true;
    }
  }
}
