using TestWeb.Models;

namespace TestWeb.Repository
{
	public class loaispRepository : IloaispRepository
	{
		private readonly QuanLiBanSachContext _context;
		public loaispRepository(QuanLiBanSachContext context)
		{
			_context = context;
		}

		public ChuDe add(ChuDe nhaXuatBan)
		{
			_context.ChuDes.Add(nhaXuatBan);
			_context.SaveChanges();
			return nhaXuatBan;
		}

		public ChuDe Delete(string manhaxuatban)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ChuDe> GetAllnhaXuatBan()
		{
			return _context.ChuDes;
		}

		public ChuDe GetNhaXuatban(string manhaxuatban)
		{
			return _context.ChuDes.Find(manhaxuatban);
		}

		public ChuDe Update(ChuDe nhaXuatBan)
		{
			_context.Update(nhaXuatBan);
			_context.SaveChanges();
			return nhaXuatBan;
		}
	}
}	