using Microsoft.AspNetCore.Components;
using E_Library.Data;
using System.Data.Common;

namespace E_Library.Pages
{
    public partial class YourBooks : ComponentBase
    {
        protected List<DataPoints> list { get; set; }
        protected override void OnInitialized()
        {
            int account = db.GetAccount(user.Email);
            list = db.GetMyBooks(account);

        }


        private void ButtonClicked(string pdf)
        {
            book.book = pdf;
            this.Read();
        }

        private void ReturnButtonClicked(int bid)
        {
            db.CheckIn(bid);
            int account = db.GetAccount(user.Email);
            list = db.GetMyBooks(account);
     
        }

        async Task Read() => NavigationManager.NavigateTo("/Read");

    }


}
