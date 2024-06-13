using BlendMaster.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BlendMaster.Services
{
    public class WineService
    {
        private readonly EnDbContext _context;

        public WineService(EnDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Wine> GetAllWines()
        {
            // 获取所有Wine对象，并创建List
            return _context.WineList.ToList();
        }

        public Wine GetWineById(int id)
        {
            // 根据WineId属性寻找第一个值为id的Wine
            return _context.WineList.FirstOrDefault(w => w.WineId == id);
        }
    }
}
