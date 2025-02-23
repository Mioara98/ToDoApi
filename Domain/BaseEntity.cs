using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
  public class BaseEntity
  {
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Name { get; set; } = string.Empty;

    public bool Deleted { get; set; } = false;
  }
}
