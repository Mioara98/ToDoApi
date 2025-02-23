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
  public class UserRepositorie : IRepositorie<User>
  {
    private IMongoCollection<User> _users;
    public UserRepositorie()
    {
      IMongoClient mongoClient = new MongoClient("mongodb+srv://Licenta2025:Licenta2025@cluster0.0fh6p.mongodb.net/");
      IMongoDatabase database = mongoClient.GetDatabase("ToDoDB");
      IMongoCollection<User> usersCollection = database.GetCollection<User>("Users");
      this._users = usersCollection;
    }

    public async Task<string> CreateAsync(User entity)
    {
      entity.Id = ObjectId.GenerateNewId().ToString();
      await this._users.InsertOneAsync(entity);
      return entity.Id;
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
      try
      {
        var filter = Builders<User>.Filter.Eq(x => x.Id, id);
        await this._users.DeleteOneAsync(filter);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public async Task<List<User>> GetAllAsync()
    {
      var filter = Builders<User>.Filter.Empty;
      var users = await this._users.Find(filter).ToListAsync();
      return users;
    }

    public async Task<User> GetByIdAsync(string id)
    {
      var filter = Builders<User>.Filter.Eq(x => x.Id, id);
      var user = await this._users.Find(filter).FirstOrDefaultAsync();
      return user;
    }

    public async Task<bool> UpdateAsync(User entity)
    {
      try
      {
        var filter = Builders<User>.Filter.Eq(x => x.Id, entity.Id);
        await this._users.ReplaceOneAsync(filter, entity);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
