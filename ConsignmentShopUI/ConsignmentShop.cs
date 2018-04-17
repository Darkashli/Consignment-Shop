using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store _store = new Store();
        private List<Item> _shoppingCartData = new List<Item>();

        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();
        private decimal storeProfit = 0;



        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = _store.Items.Where(x => x.Sold == false).ToList();
            itemsListbox.DataSource = itemsBinding;
            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            cartBinding.DataSource = _shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;
            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";

            vendorsBinding.DataSource = _store.Vendors;
            vendorListbox.DataSource = vendorsBinding;
            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";
        }   


        private void SetupData()
        {
            _store.Vendors.Add(new Vendor { FirstName = "Alaa", LastName = "DKI" });
            _store.Vendors.Add(new Vendor { FirstName = "Peter", LastName = "Snoek" });

            _store.Items.Add(new Item
            {
                Title = "The Blue Light",
                Description = "A book about a windstorm",
                Price = 25.50M,
                Owner = _store.Vendors[0]

            });

            _store.Items.Add(new Item
            {
                Title = "The Green Mile",
                Description = "A book about the life",
                Price = 20.25M,
                Owner = _store.Vendors[0]

            });

            _store.Items.Add(new Item
            {
                Title = "Harry Potter",
                Description = "A book about a boy",
                Price = 50.50M,
                Owner = _store.Vendors[1]

            });

            _store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 20.25M,
                Owner = _store.Vendors[1]

            });

            _store.Name = "Seconds are always Better";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item) itemsListbox.SelectedItem;

            _shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);
        }


        private void makePurchase_Click(object sender, EventArgs e)
        {
            foreach (Item item in _shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal) item.Owner.Commission * item.Price;
                storeProfit += (1 - (decimal) item.Owner.Commission) * item.Price;
            }

            _shoppingCartData.Clear();

            itemsBinding.DataSource = _store.Items.Where(x => x.Sold == false).ToList();

            storeProfitValue.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }


        private void itemsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    } 
}
