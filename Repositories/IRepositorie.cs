using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
  public interface IRepositorie<T>
  {
    public Task<T> GetByIdAsync(string id);

    public Task<List<T>> GetAllAsync(); 

    public Task<bool> DeleteByIdAsync(string id);

    public Task<bool> UpdateAsync(T entity);

    public Task<string> CreateAsync(T entity);
  }
}
