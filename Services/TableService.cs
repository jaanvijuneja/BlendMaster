using BlendMaster.Entities;
using System.Linq;

namespace BlendMaster.Services
{
    public class TableService
    {
        private readonly EnDbContext _context;

        public TableService(EnDbContext context)
        {
            _context = context;
        }

        public Table GetTableByName(string tableName)
        {
            // 根据TableName属性寻找第一个值为tableName的Table
            return _context.TableList.FirstOrDefault(t => t.TableName == tableName);
        }
        public Table GetTableById(int? tableId)
        {
            // 根据TableId属性寻找第一个值为tableId的Table
            return _context.TableList.FirstOrDefault(t => t.TableId == tableId);
        }

        public IEnumerable<Table> GetAllTables()
        {
            return _context.TableList.ToList();
        }
    }
}
