using Microsoft.AspNetCore.Components;
using E_Library.Data;
using System.Data.Common;

namespace E_Library.Pages
{
    public partial class MainPage : ComponentBase
    {
        public int account;
        protected  List<DataPoints> list { get; set; }
        protected override void OnInitialized()
        {
            account = db.GetAccount(user.Email);
            list = db.GetBooks();


        }

        private void ButtonClicked(int bid)
        {

            db.CheckOut(bid, account);
            list = db.GetBooks();
        }
    }
}
