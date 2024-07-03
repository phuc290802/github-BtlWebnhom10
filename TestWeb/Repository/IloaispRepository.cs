using TestWeb.Models;
namespace TestWeb.Repository
{
	public interface IloaispRepository
	{
		ChuDe add(ChuDe nhaXuatBan);
		ChuDe Update(ChuDe nhaXuatBan);
		ChuDe Delete(string manhaxuatban);
		ChuDe GetNhaXuatban(string manhaxuatban);
		IEnumerable<ChuDe> GetAllnhaXuatBan();
	}
}
