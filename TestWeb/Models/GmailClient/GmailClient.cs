namespace TestWeb.Models.GmailClient
{
    public class GmailClient
    {
        public  static string Email;
        public static string Name;
		public static int  SoSach;
		public static int MaKhachHang ;
		public string getEmail()
        {
            return Email;
        }
        public void setEmail(string email)
        {
            Email = email;
        }
        public string getName()
        {
            return Name;
        }
        public void setName(string name)
        {
            Name = name;
        }

		public int getSoSach()
		{
			return SoSach;
		}
		public void setSoSach(int sosach)
		{
			SoSach = sosach;
		}
		public int getMaKhachHang()
		{
			return MaKhachHang;
		}
		public void setMaKhachHang(int m)
		{
			MaKhachHang= m;
		}
	}
}
