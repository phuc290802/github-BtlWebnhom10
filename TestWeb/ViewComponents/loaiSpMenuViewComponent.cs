using TestWeb.Models;
using Microsoft.AspNetCore.Mvc;
using TestWeb.Repository;

namespace ProjectBookStore.ViewComponents
{
	public class loaiSpMenuViewComponent: ViewComponent
	{
		private readonly IloaispRepository _nhaXB;
		public loaiSpMenuViewComponent(IloaispRepository loaispRepository)
		{
			_nhaXB = loaispRepository;

		}
		public IViewComponentResult Invoke()
		{
			var nhaXB = _nhaXB.GetAllnhaXuatBan().OrderBy(x => x.TenChuDe);
			return View(nhaXB);
		}
	}
}
